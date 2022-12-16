using Newtonsoft.Json;

namespace MMSDemoTrucks.Models
{
    public class TrucksMaster
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Plate")]
        public string Plate { get; set; }
    }
}
