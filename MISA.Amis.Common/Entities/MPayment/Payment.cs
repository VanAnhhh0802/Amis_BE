using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.Entities.MPayment
{
    /// <summary>
    /// Lớp chi phiếu
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// ID chi phiếu
        /// </summary>
        [Key]
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Số chi phiếu
        /// </summary>
        [Required]
        public string PaymentNumber { get; set; }
        
        /// <summary>
        /// Id đối tượng
        /// </summary>
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// Id Nhân viên
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Tên Nhân viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Mã đối tượng
        /// </summary>
        public string ObjectCode { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Người nhận
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Lý do chi
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Kèm theo
        /// </summary>
        public int? Attachment { get; set; }

        /// <summary>
        /// Lý do chi theo kiểu
        /// </summary>
        public int? ReasonType { get; set; }

        /// <summary>
        /// Ngày chi phiếu
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Ngày hạch toán
        /// </summary>
        public DateTime? PostedDate { get; set; }

        /// <summary>
        /// Tổng tiền chi phiếu
        /// </summary>
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhẩt
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public string ModifiedBy { get; set; }

    }
}
