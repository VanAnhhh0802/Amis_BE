using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.Entities.DTO
{
    public class ValidateResults
    {
        /// <summary>
        /// Kết quả validate: true là không có lỗi, false là không có lỗi
        /// </summary>
        public bool success { get; set; }

        public List<string> errorResult { get; set; }
}
}
