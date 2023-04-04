using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities.MPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.PaymentBL
{
    public interface IPaymentBL : IBaseBL<Payment>
    {
        public string GetNewPaymentNumber();

        /// <summary>
        /// Hàm xuất danh sách chứng từ thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách Chứng từ</returns>
        /// Creatd By: Văn Anh (17/2/2023)
        public MemoryStream ExportPayment(string keyword);
    }
}
