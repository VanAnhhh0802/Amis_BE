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
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
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
        /// Hàm xuất danh sách nhân viên thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách nhân viên</returns>
        /// Creatd By: Văn Anh (17/2/2023)
        public List<Employee> ExportEmployee(string keyword)
        {
            string storedProcedureName = String.Format(ProcedureName.Export, typeof(Employee).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_keyword", keyword);

            //Khởi tạo kết nốt tới DB
            var  result = new List<Employee>();
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                result = mySqlConnection.Query<Employee>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return result;
        }
    }
}
