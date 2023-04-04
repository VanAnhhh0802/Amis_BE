using MISA.Amis.Common.CustomAttribute;
using MISA.Amis.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Amis.Common.CustomAttribute;
using MISA.Amis.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.Amis.Common.Entities
{
    /// <summary>
    /// Lớp thông tin tài khoản
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Id của thông tin tài khoản
        /// </summary>
        [Key]
        public Guid AccountId { get; set; }

        /// <summary>
        /// Tên tài khoản
        /// </summary>
        [Required]
        public string AccountName { get; set; }

        /// <summary>
        /// Số tài khoản
        /// </summary>
        [Required]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Bậc của tài khoản
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// ID của tài khoản cha
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Tên tiếng anh
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// Tính chẩt
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Có hạch toán ngoại tệ
        /// </summary>
        public bool? HasForeignCurrencyAccounting { get; set; }

        /// <summary>
        /// Trạng thái true- Đang sử dụng, false - Ngững sử dụng
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// LÀ đối tượng cha
        /// </summary>
        public bool? IsParent { get; set; }

        /// <summary>
        /// Theo dõi theo đối tượng
        /// </summary>
        public bool? IsTrackObject { get; set; }

        /// <summary>
        /// Theo dõi theo THCP
        /// </summary>
        public bool? IsTrackJob { get; set; }

        /// <summary>
        /// Theo dõi đơn đặt hàng
        /// </summary>
        public bool? IsTrackOrder { get; set; }

        /// <summary>
        /// THeo dõi đơn bán
        /// </summary>
        public bool? IsTrackPurchaseContract { get; set; }

        /// <summary>
        /// Theo dõi theo đơn vị
        /// </summary>
        public bool? IsTrackOrganizationUnit { get; set; }

        /// <summary>
        /// Theo dõi tài khoản ngân hàng
        /// </summary>
        public bool? IsTrackBankAccount { get; set; }

        /// <summary>
        /// Theo dõi công trình
        /// </summary>
        public bool? IsTrackProjectWork { get; set; }

        /// <summary>
        /// Theo dõi hợp đông mua
        /// </summary>
        public bool? IsTrackSaleContract { get; set; }

        /// <summary>
        /// Theo dõi khoản mục PC
        /// </summary>
        public bool? IsTrackExpenseItem { get; set; }

        /// <summary>
        /// THeo dõi mã thống kê
        /// </summary>
        public bool? IsTrackItem { get; set; }

        /// <summary>
        /// Đối tượng 0- Nhà cung cấp, 1- Khách hàng, 2- nhân viên
        /// </summary>
        public EnumObject Object { get; set; }

        /// <summary>
        /// đối tượng THCP
        /// </summary>
        public int? Job { get; set; }

        /// <summary>
        /// Đơn đặt hàng
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Hợp đồng mua
        /// </summary>
        public int? PurchaseContract { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public int? OrganizationUnit { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public int? BankAccount { get; set; }

        /// <summary>
        /// Công trình
        /// </summary>
        public int? ProjectWork { get; set; }

        /// <summary>
        /// Hợp đồng bán
        /// </summary>
        public int? SaleContract { get; set; }

        /// <summary>
        /// Khoản mục
        /// </summary>
        public int? ExpenseItem { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public int? Item { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// NGười sửa
        /// </summary>
        public string ModifiedBy { get; set; }


    }
}
