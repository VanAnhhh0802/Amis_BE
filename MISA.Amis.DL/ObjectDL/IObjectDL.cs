using MISA.Amis.Common.Entities;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL.ObjectDL
{
    public interface IObjectDL : IBaseDL<MObject>
    {
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        public List<MObject> GetObjectAll();
    }
}
