using Microsoft.AspNetCore.Mvc;
using MMSDemoTrucks.Models;
using MMSDemoTrucks.ParametersModel;
using System;

namespace MMSDemoTrucks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "SalesOrders")]
    public class SalesOrders : ControllerBase
    {

        public SalesOrders()
        {

        }
        /// <summary>
        /// Manages readonly status of dataObjs 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SelectTruckValueChanged")]
        public ActionResult<BaseResponse> SelectTruckValueChanged([FromBody] SelectTruckCarrierRequest request)
        {
            BaseResponse response = new BaseResponse();
            MA_SaleOrd_Extended saleOrd = new MA_SaleOrd_Extended();
            try
            {
                if (request is null || request.SelectCarrier is null || request.SelectTruck is null || request.Carrier is null || request.Truck is null)
                {
                    response.Success = false;
                    response.ErrorMessage = new ErrorMessage("Request is bad formatted");
                    return new OkObjectResult(response);
                }

                saleOrd.SelectTruck.value = request.SelectTruck.HasValue && request.SelectTruck.Value;
                saleOrd.SelectCarrier.value = !saleOrd.SelectTruck.value;
                saleOrd.Truck.IsReadOnly = request.SelectTruck.HasValue && !request.SelectTruck.Value;
                saleOrd.Carrier.IsReadOnly = request.SelectTruck.HasValue && request.SelectTruck.Value;
                saleOrd.Carrier.value = string.Empty;
                saleOrd.Truck.value = string.Empty;
                                
                //set returnValue on response
                response.ReturnValue = saleOrd;
                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                //throw new Exception("AssignmentAreasTrucks ExtractData exception  ", e);
                response.Success = false;
                response.ErrorMessage = new ErrorMessage("Cannot execute SelectTruckValueChanged");
                return new OkObjectResult(response);
            }
        }

        /// <summary>
        /// Manages readonly status of dataObjs 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SelectCarrierValueChanged")]
        public ActionResult<BaseResponse> SelectCarrierValueChanged([FromBody] SelectTruckCarrierRequest request)
        {
            BaseResponse response = new BaseResponse();
            MA_SaleOrd_Extended saleOrd = new MA_SaleOrd_Extended();
            try
            {
                if (request is null || request.SelectCarrier is null || request.SelectTruck is null || request.Carrier is null || request.Truck is null)
                {
                    response.Success = false;
                    response.ErrorMessage = new ErrorMessage("Request is bad formatted");
                    return new OkObjectResult(response);
                }

                saleOrd.SelectCarrier.value = request.SelectCarrier.HasValue && request.SelectCarrier.Value;
                saleOrd.SelectTruck.value = !saleOrd.SelectCarrier.value;
                saleOrd.Truck.IsReadOnly = request.SelectCarrier.HasValue && request.SelectCarrier.Value;
                saleOrd.Carrier.IsReadOnly = request.SelectCarrier.HasValue && !request.SelectCarrier.Value;
                saleOrd.Carrier.value = string.Empty;
                saleOrd.Truck.value = string.Empty;

                //set returnValue on response
                response.ReturnValue = saleOrd;
                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                //throw new Exception("AssignmentAreasTrucks ExtractData exception  ", e);
                response.Success = false;
                response.ErrorMessage = new ErrorMessage("Cannot execute SelectCarrierValueChanged");
                return new OkObjectResult(response);
            }
        }

        /// <summary>
        /// Enable/disable your data
        /// in base of corrisponding status of your document. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ControlsEnabled")]
        public ActionResult<BaseResponse> ControlsEnabled([FromBody] ControlsEnabledRequest request)
        {
            BaseResponse response = new BaseResponse();
            MA_SaleOrd_Extended saleOrd = new MA_SaleOrd_Extended();
            try
            {
               
                if (request is null || request.SelectCarrier is null || request.SelectTruck is null || request.Carrier is null || request.Truck is null)
                {
                    response.Success = false;
                    response.ErrorMessage = new ErrorMessage("Request is bad formatted");
                    return new OkObjectResult(response);
                }
                
                if (request.FormMode == 2 /*FormModeType.New*/ || request.FormMode == 3 /*FormModeType.Edit*/)
                {
                    if (request.SelectCarrier.HasValue && request.SelectCarrier.Value)
                    {
                        saleOrd.SelectCarrier.value = request.SelectCarrier.HasValue && request.SelectCarrier.Value;
                        saleOrd.SelectTruck.value = !saleOrd.SelectCarrier.value;
                        saleOrd.Truck.IsReadOnly = request.SelectCarrier.HasValue && request.SelectCarrier.Value;
                        saleOrd.Carrier.IsReadOnly = request.SelectCarrier.HasValue && !request.SelectCarrier.Value;
                        saleOrd.Carrier.value = string.Empty;
                        saleOrd.Truck.value = string.Empty;
                    }
                    if (request.SelectTruck.HasValue && request.SelectTruck.Value)
                    {
                        saleOrd.SelectTruck.value = request.SelectTruck.HasValue && request.SelectTruck.Value;
                        saleOrd.SelectCarrier.value = !saleOrd.SelectTruck.value;
                        saleOrd.Truck.IsReadOnly = request.SelectTruck.HasValue && !request.SelectTruck.Value;
                        saleOrd.Carrier.IsReadOnly = request.SelectTruck.HasValue && request.SelectTruck.Value;
                        saleOrd.Carrier.value = string.Empty;
                        saleOrd.Truck.value = string.Empty;
                    }
                }

                response.ReturnValue = saleOrd;
                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                //throw new Exception("MyDocBL ControlsEnabled exception  ", e);
                response.Success = false;
                response.ErrorMessage = new ErrorMessage("Cannot execute ControlsEnabled");
                return new OkObjectResult(response);
            }
        }
    }
}
