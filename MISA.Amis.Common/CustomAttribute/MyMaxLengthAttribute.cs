using MISA.Amis.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.CustomAttribute
{
    public class MyMaxLengthAttribute : Attribute
    {
        /// <summary>
        /// Độ dài tối đa
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// MÃ lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        public MyMaxLengthAttribute(int maxLength, ErrorCode errorCode) 
        {
            MaxLength = maxLength;
            ErrorCode = errorCode;
        }
    }
}
