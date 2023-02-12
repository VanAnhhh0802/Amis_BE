using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities.DTO;

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
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetEmployeeById,
                        UserMsg = Common.Resource.UserMsg_GetEmployeeById,
                        DevMsg = Common.Resource.DevMsg_GetEmployeeById
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

                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Common.Resource.UserMsg_Exception,
                });
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
                if (result.IsSuccess)
                {
                    //TH Thành công
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    //Th thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError, result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Common.Resource.UserMsg_Exception,
                });
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
                var result = _baseBL.Update(record);

                if (result.IsSuccess)
                {
                    //TH Thành công
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    //TH Thất bại
                    return StatusCode(StatusCodes.Status500InternalServerError,result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Common.Resource.UserMsg_Exception,
                });
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
                        ErrorCode = Common.Enums.ErrorCode.DeleteFailed,
                        DevMsg = Resource.DevMsg_Deleted,
                        UserMsg = Resource.UserMsg_Deleted,
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.UserMsg_Exception,
                });
            }
        }
        #endregion
    }
}
