using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.Entities.MPayment
{

    /// <summary>
    /// Lớp phiếu chi
    /// </summary>
    public class PaymentDetail
    {
        /// <summary>
        /// Id phiếu chi
        /// </summary>
        [Key]
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Id đối tượng
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// Tên đối tương
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Mã đối tương
        /// </summary>
        public string ObjectCode { get; set; }

        /// <summary>
        /// Tổng tiền 1 phiếu chi
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        [Required]
        public Guid DebitAccount { get; set; }

        /// <summary>
        /// Tài khoản có
        /// </summary>
        [Required]
        public Guid CreditAccount { get; set; }

        /// <summary>
        /// Diễn tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

    }
}
