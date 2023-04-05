using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.DL.AccontDL
{
    public interface IAccountDL : IBaseDL<Account>
    {
        /// <summary>
        /// lấy ra tất cả những tài khoản là con
        /// </summary>
        /// <returns>Danh sách tài khoản con</returns>
        public List<Account> GetAccountChildren();

        /// <summary>
        /// Lấy ra tất cả các account
        /// </summary>
        /// <returns>Danh sách tài khoản</returns>
        public List<Account> GetAllAccount();

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="ids">Danh sách ID tài khoản cần cập nhật</param>
        /// <param name="isAcive">Trạng thái tài khoản cần cập nhật</param>
        /// <returns>Trạng thái của tài khoản sau khi cập nhật</returns>
        public int UpdateActive(string ids, bool isAcive);

        /// <summary>
        /// Tìm kiếm tài khoản
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PagingResult<Account> Filter(string? keyword, int pageSize, int pageNumber);
    } 
}
