using MISA.Amis.Common.CustomAttribute;
using MISA.Amis.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MISA.Amis.Common.Entities
{
    /// <summary>
    ///    Thông tin nhân viên
    /// </summary>
    //Thông tin nhân viên
    public class Employee
    {
        /// <summary>
        /// KHóa chính
        /// </summary>
        [Key]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        [Unique(ErrorCode = ErrorCode.DuplicateCode)]
        [MyMaxLength(20, ErrorCode.OutMaxLengthCode)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [MyMaxLength(100, ErrorCode.OutMaxLengthName)]
        public string FullName { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        [Required(ErrorMessage = "Phòng ban không được để trống")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Date(ErrorCode = Common.Enums.ErrorCode.DateValid)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Chứng minh nhân dân
        /// </summary>
        [MyMaxLength(255, ErrorCode.OutMaxLengthIdentityNumber)]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        [Date(ErrorCode = Common.Enums.ErrorCode.DateValid)]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        [MyMaxLength(255, ErrorCode.OutMaxLengthIdentityPlace)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MyMaxLength(255, ErrorCode.OutMaxLengthAddress)]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [MyMaxLength(50, ErrorCode.OutMaxLengthPhoneNumber)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [MyMaxLength(50, ErrorCode.OutMaxLengthTelePhoneNumber)]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Email(ErrorCode = Common.Enums.ErrorCode.EmailInValidate)]
        [MyMaxLength(100, ErrorCode.OutMaxLengthEmail)]
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [MyMaxLength(100, ErrorCode.OutMaxLengthBankAccountNumber)]
        public string? BankAccountNumber { get; set; }   

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MyMaxLength(255, ErrorCode.OutMaxLengthBankName)]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [MyMaxLength(255, ErrorCode.OutMaxLengthBankBranch)]
        public string? BankBranch { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// NGười sửa
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        public Guid? PositionId { get; set; }

    }
}
