using Newtonsoft.Json;
using System;

namespace MMSDemoTrucks.ParametersModel
{
    public class BaseRequest
    {
        /// <summary>
        /// RequestId
        /// </summary>
        [JsonProperty("requestId")]
        public object RequestId { get; set; }
        /// <summary>
        /// OperationDate
        /// </summary>
        [JsonProperty("operationDate")]
        public DateTime OperationDate { get; set; }
    }
}
