using Newtonsoft.Json;

namespace MMSDemoTrucks.ParametersModel
{
    public class ErrorMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("errorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage InnerErrorMessage { get; set; }

        public ErrorMessage(string text)
        {
            Text = text;
        }
    }
}
