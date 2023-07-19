using MMSDemoTrucks.Models;
using Newtonsoft.Json;

namespace MMSDemoTrucks.Models
{
    public class CSTB9D2EFD3_00001_MA_SaleOrd_Extended
    {
        public CSTB9D2EFD3_00001_MA_SaleOrd_Extended()
        {
            this.SelectCarrier = new BaseModel<bool>();
            this.SelectTruck = new BaseModel<bool>();
            this.Carrier = new BaseModel<string>();
            this.Truck = new BaseModel<string>();
        }

        [JsonProperty("SelectCarrier")]
        public BaseModel<bool> SelectCarrier { get; set; }

        [JsonProperty("SelectTruck")]
        public BaseModel<bool> SelectTruck { get; set; }

        [JsonProperty("Carrier")]
        public BaseModel<string> Carrier { get; set; }

        [JsonProperty("Truck")]
        public BaseModel<string> Truck { get; set; }
    }
}
