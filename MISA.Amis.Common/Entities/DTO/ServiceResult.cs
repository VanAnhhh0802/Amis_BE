using MISA.Amis.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.Entities.DTO
{
    public class ServiceResult
    {
        /// <summary>
        /// true: Thành công, False: Thất bại
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Đối tượng lỗi trả về
        /// </summary>
        public dynamic? Data { get; set; }

        /// <summary>
        /// Massage mô tả lỗi
        /// </summary>
        public string? Message { get; set; }
        
        public int numberOfAffectedRows { get; set; }

    }
}
