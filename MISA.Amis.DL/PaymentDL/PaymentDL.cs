using Dapper;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL.PaymentDL
{
    public class PaymentDL : BaseDL<Payment>, IPaymentDL
    {
        public PaymentDL(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        /// <summary>
        /// Hàm xuất danh sách Chứng từ thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách chứng </returns>
        /// Creatd By: Văn Anh (17/2/2023)
        public List<Payment> ExportPayment(string keyword)
        {
            string storedProcedureName = String.Format(ProcedureName.Export, typeof(Payment).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_keyword", keyword);

            //Khởi tạo kết nốt tới DB
            var result = new List<Payment>();  
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                result = mySqlConnection.Query<Payment>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        /// <summary>
        /// Hàm lấy Số chứng từ mới
        /// </summary>
        /// <returns>
        /// Số chứng từ mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public string GetNewPaymentNumber()
        {
            string storedProcedureName = String.Format(ProcedureName.GetNewPaymentNumber);

            var parameters = new DynamicParameters();

            // Chuẩn bị tham số đầu vào cho stored

            List<decimal> noPartNumbers = new List<decimal>();
            //Khởi tạo kết nốt tới DB
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                var result = mySqlConnection.QueryMultiple(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);

                var list = result.Read<string>().ToList();

                foreach(var item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        //Lấy ra vị trí cắt chuỗi
                        int position = item.IndexOf("C");
                        decimal numberCode = decimal .Parse(item.Substring(position + 1));

                        noPartNumbers.Add(numberCode);
                    }
                }

            }

            decimal maxPaymentNumber = noPartNumbers.Max() + 1m;

            // Xử lý kết quả trả về
            return "PC" + maxPaymentNumber;
        }
    }
}
