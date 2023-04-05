using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL.PaymentDL
{
    public interface IPaymentDL : IBaseDL<Payment>
    {
        /// <summary>
        /// Hàm xuất danh sách Chứng từ thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách chứng </returns>
        /// Creatd By: Văn Anh (17/2/2023)
        public List<Payment> ExportPayment(string keyword);

        /// <summary>
        /// Lấy ra mã chứng từ mới
        /// </summary>
        /// <returns></returns>
        public string GetNewPaymentNumber();
    }
}
