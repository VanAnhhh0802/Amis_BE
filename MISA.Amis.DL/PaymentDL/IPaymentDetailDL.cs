using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL.PaymentDL
{
    public interface IPaymentDetailDL : IBaseDL<PaymentDetail>
    {
        /// <summary>
        /// Lấy ra tất cả các Chi phiếu 
        /// </summary>
        /// <returns>Danh sách Chi phiếu </returns>
        public List<PaymentDetail> GetAlLPaymentDetail();

        /// <summary>
        /// Thêm nhiều chứng từ detail
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public Guid InsertDetailMany(PaymentDetail record);


        /// <summary>
        /// Lấy chi tiết chứng từ theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PaymentDetail> GetDetailById(Guid id );

        /// <summary>
        /// Cập nhật chứng từ theo id
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        public int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails);
    }
}
