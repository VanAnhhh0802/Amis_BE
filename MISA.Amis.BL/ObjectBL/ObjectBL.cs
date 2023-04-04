using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities;
using MISA.Amis.DL.AccontDL;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.ObjectDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.ObjectBL
{
    public class ObjectBL : BaseBL<MObject> , IObjectBL
    {
        #region Field

        private IObjectDL _objectDL;

        #endregion

        #region Constructor

        public ObjectBL(IObjectDL objectDL) : base(objectDL)
        {
            _objectDL = objectDL;
        }

        #endregion

        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        public List<MObject> GetObjectAll()
        {
            return _objectDL.GetObjectAll();
        }
    }
}
