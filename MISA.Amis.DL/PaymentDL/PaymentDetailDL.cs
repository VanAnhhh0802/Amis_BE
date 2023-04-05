using Dapper;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.BaseDL;
using MySqlConnector;
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

        /// <summary>
        /// Lấy ra chi tiết chi phiếu theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PaymentDetail> GetDetailById(Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.GetDetailById);

            var parameters = new DynamicParameters();

            parameters.Add($"p_PaymentId", id);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            var result = new List<PaymentDetail>();
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.Query<PaymentDetail>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).ToList();
            }

            
            //kết quả trả về
            return result;
        }

        /// <summary>
        /// Update nhiều payment detail
        /// </summary>
        /// <param name="paymentDetails">Mảng các payment detail</param>
        /// <returns></returns>
        public int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int rowsEffected = 0;
                        foreach (var paymentDetail in paymentDetails)
                        {
                            var storedName = "Proc_PaymentDetail_Update";
                            var parameters = new DynamicParameters();
                            parameters.Add("PaymentDetailId", Guid.NewGuid());
                            parameters.Add("ModifiedBy", "Hồ Văn Anh");
                            var listProps = typeof(PaymentDetail).GetProperties();
                            foreach (var prop in listProps)
                            {
                                parameters.Add($"p_{prop.Name}", prop.GetValue(paymentDetail));
                            }
                            var numberOfAffectedRow = mySqlConnection.Execute(storedName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                            rowsEffected++;
                        }
                        if (rowsEffected != paymentDetails.Count()) transaction.Rollback();
                        transaction.Commit();
                        return rowsEffected;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
