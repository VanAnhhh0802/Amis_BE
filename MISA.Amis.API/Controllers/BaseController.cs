using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities.DTO;
using System.Collections.Generic;
using MISA.Amis.Common.Enums;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// API lấy record theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="departmentId">ID của đơn vị</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Vị trí bản ghi hiện tại</param>
        /// <returns>Đối tượng IActionResult bao gồm:
        /// -Tổng số bản ghi thỏa mãn điều kiện
        /// -Danh sách nhân viên trên 1 trang
        /// -Số trang hiền thị thỏa mãn điều kiện
        /// </returns>
        /// Created By: Văn Anh (17/1/2023)
        [HttpGet("filter")]
        public IActionResult GetRecordsFilter(
            [FromQuery] string? keyword,
            [FromQuery] Guid? departmentId,
            [FromQuery] Guid? positionId,
            [FromQuery] int pageSize,
            [FromQuery] int pageNumber
            )
        {
            try
            {
                var data = _baseBL.GetRecordFilter(keyword, departmentId, positionId, pageSize, pageNumber);
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// APi lấy ra 1 record theo Id
        /// </summary>
        /// <param name="id">id record cần lấy ra</param>
        /// <returns>danh sách record theo id</returns>
        /// Created By: Văn Anh (17/1/2023)
        [HttpGet("{id}")]
        public IActionResult GetRecordById([FromRoute] Guid id)
        {
            try
            {
                var result = _baseBL.GetRecordById(id);
                // Xử lý kết quả trả về
                if (result == null)
                {
                    //TH thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetEmployeeById,
                        UserMsg = Common.Resource.UserMsg_GetEmployeeById,
                        DevMsg = Common.Resource.DevMsg_GetEmployeeById,
                        TranceId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    //TH Thành công
                    return StatusCode(StatusCodes.Status200OK, result);
                }
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
        [HttpPost]
        public IActionResult Insert([FromBody] T record)
        {
            try
            {
                var result = _baseBL.Insert(record);
                // Xử lý kết quả trả về
                if (!result.IsSuccess)
                {
                    return ValidateFalse(result);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa đối tượng record
        /// </summary>
        /// <param name="id">Id record cần sửa</param>
        /// <param name="record">thông tin của record cần sửa</param>
        /// <returns>
        /// 200: Sửa thành công
        /// 500: Thất bại
        /// </returns>
        /// Created By: Văn Anh (17/1/2023)
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] T record)
        {
            try
            {
                var result = _baseBL.Update(record, id);
                if (!result.IsSuccess)
                {
                    return ValidateFalse(result);
                }
                if (result.numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
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

        /// <summary>
        /// API xóa record
        /// </summary>
        /// <param name="id">Id record cần xóa</param>
        /// <returns>
        /// 200: xóa thành công
        /// 500: Thất bại
        /// </returns>
        /// Created By : Văn Anh (17/1/2023)
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                int numberOfAffectedRows = _baseBL.Delete(id);
                // Xử lý kết quả trả về
                //TH Thành công
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.DeleteNotExit,
                        DevMsg = Resource.DevMsg_Deleted,
                        UserMsg = Resource.UserMsg_Deleted,
                        TranceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi cùng 1 lúc
        /// </summary>
        /// <param name="ids">Danh sách id cần xóa</param>
        /// <returns>Số lượng bản ghi bị thay đổi</returns>
        [HttpDelete("DeleteMany")]
        public IActionResult DeleteMany([FromBody] Guid[] ids)
        {
            try
            {
                var result = _baseBL.DeleteMany(ids);
                if (!result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.DeleteManyError,
                        DevMsg = Common.Resource.DevMsg_DeleteManyFailed,
                        UserMsg = Resource.UserMsg_Deleted,
                        TranceId = HttpContext.TraceIdentifier
                    });
                }
                if (result.Data > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_DeleteManyFailed,
                    UserMsg = Resource.UserMsg_DeleteFailed,
                    TranceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        protected IActionResult ValidateFalse(ServiceResult result)
        {
            switch (result.ErrorCode)
            {
                case Common.Enums.ErrorCode.EmptyValue:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.EmptyValue,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.EmailInValidate:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.UserMsg_EmailInValid,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.DuplicateCode:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.DuplicateEmployeeCode,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.OutMaxLengthCode:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.UserMsg_OutLengthEmployeeCode,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.OutMaxLengthName:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.UserMsg_OutLengthEmployeeCode,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthIdentityPlace:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthIndentityPlace,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthIdentityNumber:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthIndentityNumber,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthPhoneNumber:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthPhoneNumber,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthTelePhoneNumber:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthTelephoneNumber,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthEmail:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.UserMsg_OutLengthEmail,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthBankName:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthBankName,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });

                case Common.Enums.ErrorCode.OutMaxLengthBankBranch:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthBankBranch,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.OutMaxLengthBankAccountNumber:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.OutLengthBankNumber,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.DateValid:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.UserMsg_DateValid,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                case Common.Enums.ErrorCode.EmployeeCodeError: 
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = Resource.UserMsg_CodeError,
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
                default:
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        UserMsg = "Có lỗi xảy ra",
                        MoreInfor = result.Data,
                        TranceId = HttpContext.TraceIdentifier
                    });
            }
        }

        /// <summary>
        /// Hàm dùng chung exception
        /// </summary>
        /// <param name="ex">Exceoption trả về</param>
        /// <returns>Message lỗi</returns>
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
        #endregion
    }
}
