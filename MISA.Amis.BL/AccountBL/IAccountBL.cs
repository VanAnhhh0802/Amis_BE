using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.AccountBL
{
    public interface IAccountBL : IBaseBL<Account>
    {
        /// <summary>
        /// Lấy ra tất cả tài khoản con
        /// </summary>
        /// <returns>Danh sách các tài khoản con</returns>
        public List<Account> GetAccountChildren();

        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns>Danh sách tài khoản</returns>
        public List<Account> GetAllAccount();

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="ids">Danh sách ID tài khoản cần cập nhật</param>
        /// <param name="isAcive">Trạng thái tài khoản cần cập nhật</param>
        /// <returns>Trạng thái của tài khoản sau khi cập nhật</returns>
        public int UpdateActive(Guid[] ids, bool isAcive);
    }
}
