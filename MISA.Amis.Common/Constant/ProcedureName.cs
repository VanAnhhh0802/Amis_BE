using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.Constant
{
    public static class ProcedureName
    {
        /// <summary>
        /// ProcedureName Thêm 1 bản ghi
        /// </summary>
        public static string Insert = "Proc_{0}_Insert";

        /// <summary>
        /// ProcedureName Sửa 1 bản ghi
        /// </summary>
        public static string Update = "Proc_{0}_Update";

        /// <summary>
        ///  ProcedureName Xóa 1 bản ghi
        /// </summary>
        public static string Delete = "Proc_{0}_Delete";

        /// <summary>
        ///  ProcedureName Lấy ra mã lớn nhất trong danh sách
        /// </summary>
        public static string GetByMaxCode = "Proc_{0}_GetByMaxCode";

        /// <summary>
        ///  ProcedureName Lấy ra danh sách theo điều kiện lọc
        /// </summary>
        public static string GetFilter = "Proc_{0}_GetFilter";

        /// <summary>
        ///  ProcedureName Lấy ra chi tiết 1 bản ghi theo id
        /// </summary>
        public static string GetById = "Proc_{0}_GetById";

        /// <summary>
        /// Procudure kiểm tra mã trùng
        /// </summary>
        public static string CheckDuplicateCode = "Proc_{0}_CheckDuplicateCode";

        /// <summary>
        /// Procudure xuất khẩu
        /// </summary>
        public static string Export = "Proc_{0}_Export";

        /// <summary>
        /// Proc xóa nhiều
        /// </summary>
        public static string DeleteMany = "Proc_{0}_DeleteMany";

        /// <summary>
        /// Proc lấy ra tài khoản con
        /// </summary>
        public static string GetAccountChildren = "Proc_{0}_GetChildren";

        /// <summary>
        /// Lấy ra tất cả tài khoản
        /// </summary>
        public static string GetAllAccount = "Proc_{0}_GetAll";

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        public static string UpdateAccountAcitve = "Proc_{0}_UpdateActive";

        /// <summary>
        /// Lấy ra tất cả object
        /// </summary>
        public static string GetAllObjecct = "Proc_Object_GetAll";

        /// <summary>
        /// Lấy ra tất cả nhân viên
        /// </summary>
        public static string GetAllEmployee = "Proc_Employee_GetAll";

        /// <summary>
        /// Thêm chứng từ detail
        /// </summary>
        public static string InsertDetail = "Proc_PaymentDetail_InsertMany";

        /// <summary>
        /// Lấy chứng từ detail theo id
        /// </summary>
        public static string GetDetailById = "Proc_PaymentDetail_GetByIdMany";

        /// <summary>
        /// Lấy ra mã chứng từ mới
        /// </summary>
        public static string GetNewPaymentNumber = "Proc_Payment_GetNewPayment";

        /// <summary>
        /// Tìm kiếm tài khoản 
        /// </summary>
        public static string Filter = "Proc_Account_Filter";

    }
}
