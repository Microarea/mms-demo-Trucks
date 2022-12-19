using Newtonsoft.Json;
using System.Collections.Generic;
using MMSDemoTrucks.Models;

namespace MMSDemoTrucks.ParametersModel
{
    public class ControlsEnabledRequest : BaseRequest
    {
        /// <summary>
        /// Document FormMode
        /// </summary>
        [JsonProperty("formMode")]
        public int FormMode { get; set; } = -1;

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
