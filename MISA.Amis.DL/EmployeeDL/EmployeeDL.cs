using Dapper;
using MISA.Amis.Common.Enums;
using MISA.Amis.Common;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.DL;
using MISA.Amis.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Amis.DL.EmployeeDL
{
    public class EmployeeDL: BaseDL<Employee>, IEmployeeDL
    {
        public EmployeeDL(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public string GetNewEmployeeCode()
        {
            string storedProcedureName = String.Format(ProcedureName.GetByMaxCode, typeof(Employee).Name);

            var parameters = new DynamicParameters();

            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            dynamic result;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.QueryFirstOrDefault<string>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý kết quả trả về
            return result;
        }

        /// <summary>
        /// Phân trang theo danh sách nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="employeeFilter">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentId">id của phòng ban</param>
        /// <returns>Danh sách nhân viên và số lượng bản ghi</returns>
        /// Created by: VĂn Anh (6/2/2023)
        public GridReader GetEmployeeFilter(int pageSize = 10, int pageNumber = 1, string? employeeFilter = "", Guid? departmentId = null)
        {
            string storedProcedureName = String.Format(ProcedureName.GetFilter, typeof(Employee).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_DepartmentId", departmentId);
            parameters.Add("p_EmployeeFilter", employeeFilter);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            dynamic multy;

            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();

                //Lấy kêt quả theo result
                //Gọi vào Db
                 multy = mySqlConnection.QueryMultiple(
                   storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý kết quả trả về

            return multy;
        }
    }
}
