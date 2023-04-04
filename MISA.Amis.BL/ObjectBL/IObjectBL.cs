using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.ObjectBL
{
    public interface IObjectBL : IBaseBL<MObject>
    {
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        public List<MObject> GetObjectAll();
    }
}
