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

        public Guid InsertDetailMany(PaymentDetail record);


        public PaymentDetail GetDetailById(Guid id );


    }
}
