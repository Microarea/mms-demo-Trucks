using Newtonsoft.Json;

namespace MMSDemoTrucks.Models
{
    public class SalesAreas
    {
        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
