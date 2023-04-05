using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL.ObjectBL;
using MISA.Amis.BL.PaymentBL;
using MISA.Amis.BL.PaymentDetailBL;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.PaymentDL;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentDetailsController : BaseController<PaymentDetail>
    {
        #region Filed

        private IPaymentDetailBL _paymentdetailBL;

        #endregion
        #region Constructor
        public PaymentDetailsController(IPaymentDetailBL paymentdetailBL) : base(paymentdetailBL)
        {
            _paymentdetailBL = paymentdetailBL;
        }
        #endregion

        /// <summary>
        /// API Thêm record
        /// </summary>
        /// <param name="record">Nhập vào các thông tin record</param>
        /// <returns>
        /// 201: Thêm thành công 
        /// 500: Thất bại
        /// </returns>
        /// Created By Văn ANh(17/01/2023) 
        [HttpPost("InsertMany")]
        public IActionResult InsertMany([FromBody] PaymentDetail[] detail)
        {
            try
            {
                var result = _paymentdetailBL.InsertMany(detail);
                // Xử lý kết quả trả về
                if (!result.IsSuccess)
                {
                    return HandleException(result.Data);
                }
                return StatusCode(StatusCodes.Status201Created, result.Data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API Thêm record
        /// </summary>
        /// <param name="record">Nhập vào các thông tin record</param>
        /// <returns>
        /// 201: Thêm thành công 
        /// 500: Thất bại
        /// </returns>
        /// Created By Văn ANh(17/01/2023) 
        [HttpGet("getDetail/{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var result = _paymentdetailBL.GetDetailByiId(id);
                // Xử lý kết quả trả về
                
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _paymentdetailBL.GetAlLPaymentDetail();
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa nhiều payment detail
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        [HttpPut("UpdatePaymentDetails")]
        public IActionResult UpdatePaymentDetail([FromBody] IEnumerable<PaymentDetail> paymentDetails)
        {
            try
            {
                var result = _paymentdetailBL.UpdatePaymentDetails(paymentDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
                //var res = new ErrorResult
                //{
                //    DevMsg = ex.Message,
                //    UserMsg = 
                //};
                //return StatusCode(StatusCodes.Status500InternalServerError, );
            }
        }

        /// <summary>
        /// Xử  lý lỗi trả vê
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected IActionResult HandleException(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
            {
                ErrorCode = Common.Enums.ErrorCode.Exception,
                DevMsg = Resource.DevMsg_Exception,
                UserMsg = Resource.UserMsg_Exception,
                MoreInfor = ex.Message,
                TranceId = HttpContext.TraceIdentifier
            });
        }


    }
}
