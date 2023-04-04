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
    public class PaymentDetailDL : BaseDL<PaymentDetail>, IPaymentDetailDL
    {
        public PaymentDetailDL(IDatabaseConnection databaseConnection) : base(databaseConnection)
        {
        }

        /// <summary>
        /// Lấy ra tất cả các Chi phiếu 
        /// </summary>
        /// <returns>Danh sách Chi phiếu </returns>
        public List<PaymentDetail> GetAlLPaymentDetail()
        {
            string storedProcedureName = String.Format(ProcedureName.GetAllAccount, typeof(PaymentDetail).Name);

            var parameters = new DynamicParameters();

            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            List<PaymentDetail> result;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.Query<PaymentDetail>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return result;
        }


        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// -201: insert thành công
        /// -500: insert thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public Guid InsertDetailMany(PaymentDetail detail)
        {
            string storedProcedureName = String.Format(ProcedureName.InsertDetail);

            var properties = typeof(PaymentDetail).GetProperties();
            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(detail));
            }

            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
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

            if (numberOfAffectedRows > 0)
            {
                return detail.PaymentId;
            }
            //kết quả trả về
            return Guid.Empty;
        }

        public PaymentDetail GetDetailById(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.GetDetailById);

            var properties = typeof(PaymentDetail).GetProperties();
            var parameters = new DynamicParameters();

            parameters.Add($"p_PaymentId", id);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            dynamic result;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.Execute(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);
            }

            
            //kết quả trả về
            return result;
        }
    }
}
