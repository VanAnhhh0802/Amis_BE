using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.PaymentDetailBL
{
    public interface IPaymentDetailBL : IBaseBL<PaymentDetail>
    {
        public List<PaymentDetail> GetAlLPaymentDetail();

        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public ServiceResult InsertMany(PaymentDetail[] details);
        public List<PaymentDetail> GetDetailByiId(Guid[] ids);

    }
}
