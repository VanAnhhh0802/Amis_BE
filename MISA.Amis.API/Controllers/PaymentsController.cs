using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.PaymentBL;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : BaseController<Payment>
    {
        #region Filed

        private IPaymentBL _paymentBL;

        #endregion
        #region Constructor
        public PaymentsController(IPaymentBL paymentBL) : base(paymentBL)
        {
            _paymentBL = paymentBL;
        }
        #endregion

        /// <summary>
        /// Xuất excel danh sách chứng từ
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost("export")]
        public IActionResult Export([FromBody] string keyword)
        {
            try
            {
                var memoryStream = _paymentBL.ExportPayment(keyword);

                try
                {
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh sách chứng từ.xlsx");
                }
                finally
                {

                    memoryStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API Sinh mã nhân viên mới   
        /// </summary>
        /// <returns>Hiển thị ra mã nhân viên mới</returns>
        /// Created By: Văn Anh (1/2/2023)
        [HttpGet("newPaymentNumber")]
        public IActionResult GetNewPaymentNumber()
        {
            try
            {
                var newNumber = _paymentBL.GetNewPaymentNumber();
                // Xử lý kết quả trả về
                if (newNumber != null)
                {
                    //TH Thành công
                    return StatusCode(StatusCodes.Status200OK, newNumber);
                }
                else
                {
                    //Th thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.NewEmployeeCode,
                        DevMsg = Common.Resource.DevMsg_GetNewCode,
                        UserMsg = Common.Resource.UserMsg_GetNewCode,
                    });
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            //}
        }
        /// <summary>
        /// Hàm trả về mã lỗi
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
