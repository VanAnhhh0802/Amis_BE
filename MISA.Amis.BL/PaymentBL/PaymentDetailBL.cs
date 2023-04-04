using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.DL.PaymentDL;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Amis.BL.PaymentDetailBL;
using MISA.Amis.Common.Entities.DTO;

namespace MISA.Amis.BL.PaymentBL
{
    public class PaymentDetailBL : BaseBL<PaymentDetail>, IPaymentDetailBL
    {
        #region Field

        private IPaymentDetailDL _paymentDetailDL;


        #endregion

        #region Constructor
        public PaymentDetailBL(IPaymentDetailDL paymentDetailDL) : base(paymentDetailDL)

        {
            _paymentDetailDL = paymentDetailDL;
        }

        #endregion

        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public ServiceResult InsertMany(PaymentDetail[] details)
        {

             List<Guid> resultDL = new List<Guid>();
            //Guid resultDL;
            foreach (var detail in details)
            {
                resultDL.Add(_paymentDetailDL.InsertDetailMany(detail));
            }
            // Xử lý kết quả trả về

            return new ServiceResult
            {
                IsSuccess = true,
                Data = resultDL,
            };
        }

        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public List<PaymentDetail> GetDetailByiId(Guid[] ids)
        {

             List<PaymentDetail> resultDL = new List<PaymentDetail>();
            foreach (var id in ids)
            {
                resultDL.Add(_paymentDetailDL.GetDetailById(id));
            }

            // Xử lý kết quả trả về

            return resultDL;
        }

        public List<PaymentDetail> GetAlLPaymentDetail()
        {
            return _paymentDetailDL.GetAlLPaymentDetail();
        }
        
        
    }
}
