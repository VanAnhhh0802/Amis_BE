using Dapper;
using MISA.Amis.Common.Enums;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.EmployeeDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Amis.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL( IEmployeeDL employeeDL):base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion
        
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public string GetNewEmployeeCode()
        {
            var maxEmployeeCode = _employeeDL.GetNewEmployeeCode();
            string maxEmployeeCodeNumber = Regex.Replace(maxEmployeeCode, "[^0-9]", "");
            string maxEmployeeCodeString = Regex.Replace(maxEmployeeCode, "[0-9]", "");

            string newEmployeeCodeNumber = long.Parse(maxEmployeeCodeNumber) + 1 + "";

            string newEmployeeCode = maxEmployeeCodeString  + newEmployeeCodeNumber;
            return newEmployeeCode;
        }

        /// <summary>
        /// Phân trang theo danh sách nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="employeeFilter">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentID">id của phòng ban</param>
        /// <returns>Danh sách nhân viên và số lượng bản ghi</returns>
        /// Created by: VĂn Anh (6/2/2023)
        public GridReader GetEmployeeFilter(int pageSize = 10, int pageNumber = 1, string? employeeFilter = "", Guid? departmentId = null)
        {
            return _employeeDL.GetEmployeeFilter();

        }
    }
}
