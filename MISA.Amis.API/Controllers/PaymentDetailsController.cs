using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL.ObjectBL;
using MISA.Amis.BL.PaymentDetailBL;
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
                    return ValidateFalse(result);
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
        [HttpGet("Id")]
        public IActionResult GetById([FromBody] Guid[] ids)
        {
            try
            {
                var result = _paymentdetailBL.GetDetailByiId(ids);
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



    }
}
