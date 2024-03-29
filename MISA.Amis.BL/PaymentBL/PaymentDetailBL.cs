﻿using MISA.Amis.BL.BaseBL;
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
using MISA.Amis.Common.Enums;
using MISA.Amis.Common;
using MISA.Amis.PaymentBL;

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
        public List<PaymentDetail> GetDetailByiId(Guid id)
        {
            return _paymentDetailDL.GetDetailById(id);
        }

        /// <summary>
        /// Lẩy ra tất cả phiếu chi
        /// </summary>
        /// <returns></returns>
        public List<PaymentDetail> GetAlLPaymentDetail()
        {
            return _paymentDetailDL.GetAlLPaymentDetail();
        }

        /// <summary>
        /// Update nhiều payment detail
        /// </summary>
        /// <param name="paymentDetails">Mảng các payment detail</param>
        /// <returns></returns>
        public int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails)
        {
            var result = _paymentDetailDL.UpdatePaymentDetails(paymentDetails);
            return result;
        }

        
    }
}
