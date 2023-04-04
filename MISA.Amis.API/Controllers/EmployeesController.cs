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
                return HandleException(ex);
            }
            //}
        }

        [HttpPost("export")]
        public IActionResult Export([FromBody] string keyword) 
        {
            try
            {
                var memoryStrean = _employeeBL.ExportEmployee(keyword);

                try
                {
                    return File(memoryStrean.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh sách nhân viên.xlsx");
                }
                finally 
                {

                    memoryStrean.Dispose();
                }
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
                var allEmployee = _employeeBL.GetEmployeeAll();
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, allEmployee);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

    }
}
