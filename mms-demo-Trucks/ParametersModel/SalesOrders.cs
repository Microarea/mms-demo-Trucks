using Newtonsoft.Json;

namespace MMSDemoTrucks.ParametersModel
{
    public class SelectTruckCarrierRequest : BaseRequest
    {
        [JsonProperty("SelectCarrier")]
        public bool? SelectCarrier { get; set; }

        [JsonProperty("SelectTruck")]
        public bool? SelectTruck { get; set; }

        [JsonProperty("Carrier")]
        public string? Carrier { get; set; }

        [JsonProperty("Truck")]
        public string? Truck { get; set; }
    }
}
