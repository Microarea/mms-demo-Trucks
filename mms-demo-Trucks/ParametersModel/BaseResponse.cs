using Newtonsoft.Json;

namespace MMSDemoTrucks.ParametersModel
{
    public class BaseResponse
    {
        /// <summary>
        /// ReturnValue
        /// </summary>
        [JsonProperty("returnValue")]
        public object ReturnValue { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; } = true;
        /// <summary>
        /// ErrorMessage
        /// </summary>
        [JsonProperty("errorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage ErrorMessage { get; set; }
    }
}
