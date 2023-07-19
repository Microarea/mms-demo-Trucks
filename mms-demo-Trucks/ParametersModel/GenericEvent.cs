using Newtonsoft.Json;

namespace MMSDemoTrucks.ParametersModel
{
    public class EventRequest : BaseRequest
    {
        [JsonProperty("eventName")]
        public string EventName { get; set; }
    }

    public class EventResponse : BaseResponse
    {

    }
}
