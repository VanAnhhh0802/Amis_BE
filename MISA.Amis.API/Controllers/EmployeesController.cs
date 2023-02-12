using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MySqlConnector;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using MISA.Amis.BL;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.DL.EmployeeDL;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {

        #region Filed

        private IEmployeeBL _employeeBL;

        #endregion
        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL):base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// API lấy nhân viên theo bộ lọc và phân trang
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
        public IActionResult GetEmployeesFilter(
                    [FromQuery] string? keyword,
                    [FromQuery] Guid? departmentId,
                    [FromQuery] int pageSize,
                    [FromQuery] int pageNumber
                    )
        {
            try
            {
                var data = _employeeBL.GetEmployeeFilter().Read<Employee>().ToList();
                int totalRecords = _employeeBL.GetEmployeeFilter().ReadFirstOrDefault<int>();
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, new PagingResult
                {
                    Data = data,
                    totalRecord = totalRecords,
                    totalPage = totalRecords / pageSize
                });
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
        /// API Sinh mã nhân viên mới   
        /// </summary>
        /// <returns>Hiển thị ra mã nhân viên mới</returns>
        /// Created By: Văn Anh (1/2/2023)
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeBL.GetNewEmployeeCode();
                // Xử lý kết quả trả về
                if (newEmployeeCode != null)
                {
                    //TH Thành công
                    return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
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
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Common.Enums.ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Common.Resource.UserMsg_Exception,
                });
            }
            //}
            #endregion
        }
    }
}
