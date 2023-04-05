using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Enums;
using MISA.Amis.DL.AccontDL;
using MISA.Amis.DL.EmployeeDL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Amis.BL.AccountBL
{
    public class AccountBL : BaseBL<Account>, IAccountBL
    {
        #region Field

        private IAccountDL _accountDL;

        #endregion

        #region Constructor

        public AccountBL(IAccountDL accountDL) : base(accountDL)
        {
            _accountDL = accountDL;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra tất cả các tài khoản con
        /// </summary>
        /// <returns>Danh sách tài khoản con</returns>
        public List<Account> GetAccountChildren()
        {
            return _accountDL.GetAccountChildren();
        }

        /// <summary>
        /// Lấy tất cả các tài khoản
        /// </summary>
        /// <returns>Danh sách tài khoản</returns>
        public List<Account> GetAllAccount()
        {
            return _accountDL.GetAllAccount();
        }

        /// <summary>
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="ids">Danh sách ID tài khoản cần cập nhật</param>
        /// <param name="isAcive">Trạng thái tài khoản cần cập nhật</param>
        /// <returns>Trạng thái của tài khoản sau khi cập nhật</returns>
        public int UpdateActive(Guid[] ids, bool isAcive)
        {

            //var jsonIds = JsonConvert.SerializeObject(ids);
            var listRecordsId =  $"{String.Join(",", ids)}";
            return _accountDL.UpdateActive(listRecordsId, isAcive);
        }

        /// <summary>
        /// Phân trang theo danh sách record
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="keyword">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentId">id của phòng ban</param>
        /// <param name="positionId">id của Chức vụ</param>
        /// <returns>Danh sách record và số lượng bản ghi theo điều kiện</returns>
        /// Created by: VĂn Anh (6/2/2023)
        public PagingResult<Account> Filter(string keyword, int pageSize, int pageNumber)
        {
            return _accountDL.Filter(keyword,  pageSize, pageNumber);
        }

        /// <summary>
        /// Hàm validate riêng cho class account
        /// </summary>
        /// <param name="account">đối tượng account</param>
        /// <returns>ServiceResult</returns>
        protected override ServiceResult ValidateCustom(Account? account, Guid id)
        {
             
            //Check số tài khoản
            if (account.AccountNumber != null && !string.IsNullOrEmpty(account.AccountNumber?.ToString()))
            {
                //Số tài khoản trùng
                var duplicateCode = CheckDuplicate(account, (string)account.AccountNumber, id);
                if (duplicateCode)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = Common.Enums.ErrorCode.DuplicateCode,
                        Message = "Số tài khoản <" + account.AccountNumber.ToString() + "> bị trùng"
                    };
                }
                
                
            }
            
            return new ServiceResult
            {
                IsSuccess = true,
            };
        }

        #endregion
    }
}
