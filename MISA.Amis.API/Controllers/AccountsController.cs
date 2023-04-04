using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL;
using MISA.Amis.BL.AccountBL;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account>
    {
        #region Filed

        private IAccountBL _accountBL;

        #endregion
        #region Constructor
        public AccountsController(IAccountBL accountBL) : base(accountBL)
        {
            _accountBL = accountBL;
        }
        #endregion

        /// <summary>
        /// Lấy tất cả các tài khoản con
        /// </summary>
        /// <returns>Danh sách tài khoản con</returns>
        [HttpGet("AccountChildren")]
        public IActionResult GetAccountChildren()
        {
            try
            {
                var listChildren = _accountBL.GetAccountChildren();
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, listChildren);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Lấy tất cả các tài khoản
        /// </summary>
        /// <returns>Danh sách tất cả các tài khoản</returns>
        [HttpGet("All")]
        public IActionResult GetAllAccount()
        {
            try
            {
                var allAccount = _accountBL.GetAllAccount();
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, allAccount);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="ids">ID tài khoản cần cập nhật</param>
        /// <param name="isAcive">Trạng thái tài khoản cần cập nhật</param>
        /// <returns>Trạng thái của tài khoản sau khi cập nhật</returns>
        [HttpPut("UpdateActive")]
        public IActionResult UpdateActive([FromBody] Guid[] ids, [FromQuery] bool isActive )
        {
            try
            {
                var result =_accountBL.UpdateActive(ids, isActive);
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.UpdateNotExit,
                    DevMsg = Resource.UpdateNotExit,
                    UserMsg = Resource.UpdateNotExit,
                    TranceId = HttpContext.TraceIdentifier
                });

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
