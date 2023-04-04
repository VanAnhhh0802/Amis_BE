using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.Entities
{
    /// <summary>
    /// Lớp đối tượng
    /// </summary>
    public class MObject
    {
        /// <summary>
        /// Id đối tượng
        /// </summary>
        [Key]
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Mã đối tượng
        /// </summary>
        public string ObjectCode { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// NGười tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string ModifiedBy { get; set; }

    }
}
