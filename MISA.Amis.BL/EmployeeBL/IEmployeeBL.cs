using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Amis.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        string GetNewEmployeeCode();

        /// <summary>
        /// Hàm xuất danh sách nhân viên thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách nhân viên</returns>
        /// Creatd By: Văn Anh (17/2/2023)
        MemoryStream ExportEmployee(string keyword);

        public List<Employee> GetEmployeeAll();
    }
}
