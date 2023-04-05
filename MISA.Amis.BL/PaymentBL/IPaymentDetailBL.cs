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

        /// <summary>
        /// Lấy chứng từ theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created By: Văn Anh (1/4/2023)
        public List<PaymentDetail> GetDetailByiId(Guid id);

        /// <summary>
        /// Cập nhật chứng từ detail
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        /// Created By: Văn Anh (1/4/2023)
        public int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails);

    }
}
