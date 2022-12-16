using Newtonsoft.Json;

namespace MMSDemoTrucks.Models
{
    public class FP_SalesAreaTrucks
    {
        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("TruckCode")]
        public string TruckCode { get; set; }
    }
}
