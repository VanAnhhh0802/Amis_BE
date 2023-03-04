using MISA.Amis.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Common.CustomAttribute
{
    public class UniqueAttribute : Attribute
    {
        public ErrorCode ErrorCode { get; set; }
    }
}
