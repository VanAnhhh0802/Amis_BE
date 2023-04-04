using Dapper;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Amis.DL.AccontDL
{
    public class AccountDL : BaseDL<Account>, IAccountDL
    {
        public AccountDL(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        /// <summary>
        /// lấy ra tất cả những tài khoản là con
        /// </summary>
        /// <returns>Danh sách tài khoản con</returns>
        public List<Account> GetAccountChildren() 
        {
            string storedProcedureName = String.Format(ProcedureName.GetAccountChildren, typeof(Account).Name);

            var parameters = new DynamicParameters();

            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            List<Account> result;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.Query<Account>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
             return result;
            // Xử lý kết quả trả về
        }
        
        /// <summary>
        /// Lấy ra tất cả các tài khoản 
        /// </summary>
        /// <returns>Danh sách tài khoản </returns>
        public List<Account> GetAllAccount()
        {
            string storedProcedureName = String.Format(ProcedureName.GetAllAccount, typeof(Account).Name);

            var parameters = new DynamicParameters();

            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            List<Account> result;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.Query<Account>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="ids">Danh sách ID tài khoản cần cập nhật</param>
        /// <param name="isAcive">Trạng thái tài khoản cần cập nhật</param>
        /// <returns>Trạng thái của tài khoản sau khi cập nhật</returns>
        public int UpdateActive(string ids, bool isAcive)
        {
            string storedProcedureName = String.Format(ProcedureName.UpdateAccountAcitve, typeof(Account).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_AccountIds", ids);
            parameters.Add("p_AccountActive", isAcive);

            // Chuẩn bị tham số đầu vào cho stored
            int numberOfAffectedRows;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                numberOfAffectedRows = mySqlConnection.Execute(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý kết quả trả về
            return numberOfAffectedRows;
        }
    
       
    }
}
