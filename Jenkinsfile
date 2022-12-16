pipeline {
 environment   {
      VERSION = "2.1.0"
      SUFFIX = "-develop"
      WORKFOLDER = "$WORKSPACE"
      DOCKERBUILD = "true"
      FRONTEND = ""
      FRONTENDDIR = ""
      LATEST_TAG = "on"
      GIT_REPO = sh(script: 'echo "${JOB_NAME%%/*}"', returnStdout: true).trim()
      TAG = "$VERSION.$BUILD_NUMBER"
      PROJECT_TAG = "$GIT_REPO » $GIT_BRANCH #$TAG"      
      SLACKMESSAGE_SUCCESS = "*$PROJECT_TAG* - build successfully completed (<$BUILD_URL|Open>)"
      SLACKMESSAGE_FAILURE = "*$PROJECT_TAG* - build ended with errors (<$BUILD_URL|Open>)"
      SLACKPAYLOAD_SUCCESS = "payload={\"channel\": \"$SLACKCHANNEL\",\"username\": \"$PROJECT_TAG\",\"icon_emoji\": \"$SLACKEMOJI\",\"attachments\": [{\"text\": \"$SLACKMESSAGE_SUCCESS\",\"color\": \"$COLORSUCCESS\",\"mrkdwn_in\": [ \"text\" ]}] }"
      SLACKPAYLOAD_FAILURE = "payload={\"channel\": \"$SLACKCHANNEL\",\"username\": \"$PROJECT_TAG\",\"icon_emoji\": \"$SLACKEMOJI\",\"attachments\": [{\"text\": \"$SLACKMESSAGE_FAILURE\",\"color\": \"$COLORFAIL\",\"mrkdwn_in\": [ \"text\" ]}] }"
  }

  agent any
    stages {
        stage('Starting build notification') {
          environment {
            SLACKMESSAGE_START = ">>> *$PROJECT_TAG* - Started by GitHub push (<$BUILD_URL|Open>)"
            SLACKPAYLOAD_START ="payload={\"channel\": \"$SLACKCHANNEL\", \"color\": \"#D42A17\",\"username\": \"$PROJECT_TAG\", \"text\": \" $SLACKMESSAGE_START \", \"icon_emoji\": \"$SLACKEMOJI\"}"
          }
          steps {
            sh 'curl -X POST --data-urlencode "$SLACKPAYLOAD_START" "$SLACKHOOK"'
          }
        }
        stage('Docker build') {
        when {  environment name: 'DOCKERBUILD', value: 'true' }
          steps {
            sh 'docker build --build-arg version=$TAG -t microarea/$GIT_REPO:$TAG$SUFFIX --no-cache=true --pull=true $WORKFOLDER'
          }
        }
        stage('Docker push') {
        when {  environment name: 'DOCKERBUILD', value: 'true' }
          steps {
            sh 'docker login --username $DOCKERLOGINUSR --password $DOCKERLOGINPWD'
            sh 'docker push microarea/$GIT_REPO:$TAG$SUFFIX'
          }
        }
        stage('Latest tag') {
        when {
          environment name: 'DOCKERBUILD', value: 'true'
          environment name: 'LATEST_TAG', value: 'on'
        }
          steps {
            sh 'docker tag microarea/$GIT_REPO:$TAG$SUFFIX microarea/$GIT_REPO:latest$SUFFIX'
            sh 'docker push microarea/$GIT_REPO:latest$SUFFIX'
          }
        }
        stage('Frontend build external') {
        when {  environment name: 'FRONTEND', value: 'external' }
          steps {
            dir("$FRONTENDDIR"){
              sh 'export AWS_CONFIG_FILE=$AWS_CONFIG_FILE'
              sh 'export AWS_ACCESS_KEY_ID=$M4CLAB_AWS_ACCESS_KEY_ID'
              sh 'export AWS_SECRET_ACCESS_KEY=$M4CLAB_AWS_SECRET_ACCESS_KEY'
              sh 'export AWS_SECRET_ACCESS_KEY=$AWS_DEFAULT_REGION'
              sh 'sudo npm install'
              sh "sudo npm i '@progress/kendo-angular-buttons' '@progress/kendo-angular-inputs' '@progress/kendo-angular-dropdowns' npm i '@progress/kendo-angular-intl' '@progress/kendo-angular-l10n'"
              sh 'mkdir $FRONTENDDIR/output/'
              sh 'ng build --base-href . --output-path $FRONTENDDIR/output/'
              sh 'cd $FRONTENDDIR/output/ && zip -9 -r ../$GIT_REPO-$TAG$SUFFIX.zip *'
              sh 'aws s3 cp $FRONTENDDIR/$GIT_REPO-$TAG$SUFFIX.zip s3://$GIT_REPO/'
            }
          }
        }
        stage('Frontend build container') {
        when {  environment name: 'FRONTEND', value: 'container' }
          steps {
            dir("$WORKFOLDER"){
              sh 'export AWS_CONFIG_FILE=$AWS_CONFIG_FILE'
              sh 'export AWS_ACCESS_KEY_ID=$M4CLAB_AWS_ACCESS_KEY_ID'
              sh 'export AWS_SECRET_ACCESS_KEY=$M4CLAB_AWS_SECRET_ACCESS_KEY'
              sh 'export AWS_SECRET_ACCESS_KEY=$AWS_DEFAULT_REGION'
              sh 'docker login --username $DOCKERLOGINUSR --password $DOCKERLOGINPWD'
              sh 'docker run -d --name fecontainer microarea/$GIT_REPO:$TAG$SUFFIX'
              sh 'mkdir $WORKFOLDER/frontend/'
              sh 'docker cp fecontainer:/$FRONTENDDIR/. $WORKFOLDER/frontend/'
              sh 'cd $WORKFOLDER/frontend/ && zip -9 -r ../$GIT_REPO-$TAG$SUFFIX.zip *'
              sh 'aws s3 cp $WORKFOLDER/$GIT_REPO-$TAG$SUFFIX.zip s3://$GIT_REPO-frontend/'
            }
          }
        }
        stage('Frontend external latest tag') {
        when {
          environment name: 'FRONTEND', value: 'external'
          environment name: 'LATEST_TAG', value: 'on'
        }
          steps {
            sh 'sudo mv $WORKFOLDER/$GIT_REPO-$TAG$SUFFIX.zip $WORKFOLDER/$GIT_REPO-latest$SUFFIX.zip'
            sh 'aws s3 cp $WORKFOLDER/$GIT_REPO-latest$SUFFIX.zip s3://$GIT_REPO/'
          }
        }
        stage('Frontend container latest tag') {
        when {
          environment name: 'FRONTEND', value: 'container'
          environment name: 'LATEST_TAG', value: 'on'
        }
          steps {
            sh 'sudo mv $WORKFOLDER/$GIT_REPO-$TAG$SUFFIX.zip $WORKFOLDER/$GIT_REPO-latest$SUFFIX.zip'
            sh 'aws s3 cp $WORKFOLDER/$GIT_REPO-latest$SUFFIX.zip s3://$GIT_REPO-frontend/'
          }
        }
        stage('Frontend cleanup') {
        when {  environment name: 'FRONTEND', value: 'external' }
          steps {
            dir("$WORKFOLDER"){
              sh 'cd $FRONTENDDIR'
              sh 'sudo npm install rimraf -g'
              sh 'sudo rimraf node_modules'
            }
          }
        }
        stage('Frontend cleanup container') {
        when {  environment name: 'FRONTEND', value: 'container' }
          steps {
            dir("$WORKFOLDER"){
              sh 'docker stop fecontainer'
              sh 'sleep 5s'
              sh 'docker rm fecontainer'
            }
          }
        }
        stage('Docker images cleanup') {
        when {  environment name: 'DOCKERBUILD', value: 'true' }
          steps {
            sh 'docker rmi microarea/$GIT_REPO:$TAG$SUFFIX -f'
            sh 'docker rmi microarea/$GIT_REPO:latest$SUFFIX -f'
          }
        }       
    }
    post {
      success {
        sh 'git tag -a -m "$GIT_REPO v. $TAG built successfully" cloud/v$TAG'
        sh 'git config user.email $GITMAIL'
        sh 'git config user.name $GITUSER'
        sh 'git push https://$GITTOKEN@github.com/Microarea/$GIT_REPO.git cloud/v$TAG'
        sh 'curl -X POST --data-urlencode "$SLACKPAYLOAD_SUCCESS" "$SLACKHOOK"'
        cleanWs()
      }
      failure { 
        sh 'curl -X POST --data-urlencode "$SLACKPAYLOAD_FAILURE" "$SLACKHOOK"'
        cleanWs()
      }
    }
}
