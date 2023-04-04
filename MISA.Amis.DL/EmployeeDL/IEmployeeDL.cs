using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Amis.DL.EmployeeDL
{
    public interface IEmployeeDL: IBaseDL<Employee>
    {
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public string GetNewEmployeeCode();

        /// <summary>
        /// Hàm xuất danh sách nhân viên thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách nhân viên</returns>
        /// Creatd By: Văn Anh (17/2/2023)
        List<Employee> ExportEmployee(string keyword);

         List<Employee> GetEmployeeAll();
    }
}
