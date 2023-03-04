namespace MISA.Amis.Common.Enums
{   
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi Exception
        /// </summary>
        Exception = 0,

        /// <summary>
        /// Lỗi khi thêm 
        /// </summary>
        InsertFailed = 1,
        
        /// <summary>
        /// Lỗi xóa không thành công
        /// </summary>
        DeleteFailed = 2,

        /// <summary>
        /// Mã lỗi sửa không thành công
        /// </summary>
        UpdateFailed = 3,

        /// <summary>
        /// Mã lỗi của khi lấy mã mới
        /// </summary>
        NewEmployeeCode = 4,

        /// <summary>
        /// Lỗi lấy thông tin nhân viên theo id
        /// </summary>
        GetEmployeeById = 5,

        /// <summary>
        /// Mã lỗi để trống các trường bắt buộc nhập
        /// </summary>
        EmptyValue = 6,

        /// <summary>
        /// Mã lỗi mã bị trùng
        /// </summary>
        DuplicateCode = 7,

        /// <summary>
        /// Mã lỗi xóa nhiều 
        /// </summary>
        DeleteManyError = 8,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường mã
        /// </summary>
        OutMaxLengthCode = 9,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường Tên
        /// </summary>
        OutMaxLengthName = 10,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường số Chứng minh 
        /// </summary>
        OutMaxLengthIdentityNumber = 11,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường ngày cấp
        /// </summary>
        OutMaxLengthIdentityDate = 12,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường nơi cấp
        /// </summary>
        OutMaxLengthIdentityPlace = 13,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường địa chỉ
        /// </summary>
        OutMaxLengthAddress = 14,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường số điện thoại di dộng
        /// </summary>
        OutMaxLengthPhoneNumber = 15,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường SĐT cố định
        /// </summary>
        OutMaxLengthTelePhoneNumber = 16,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường Email
        /// </summary>
        OutMaxLengthEmail = 17,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường Số tài khoản ngân hàng
        /// </summary>
        OutMaxLengthBankAccountNumber = 18,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường Tên ngân hàng
        /// </summary>
        OutMaxLengthBankName = 19,

        /// <summary>
        /// Mã lỗi độ dài quá lớn của trường chi nhánh
        /// </summary>
        OutMaxLengthBankBranch = 20,

        /// <summary>
        /// Mã email không đúng định dạng
        /// </summary>
        EmailInValidate = 21,

        /// <summary>
        /// Mã lỗi ngày không được lớn hơn ngày hiện tại
        /// </summary>
        DateValid = 22,

        /// <summary>
        /// Không tồn tại update
        /// </summary>
        UpdateNotExit = 24,

        /// <summary>
        /// Không tồn tại xóa
        /// </summary>
        DeleteNotExit = 25,

        /// <summary>
        /// Mã lỗi xóa nhiều thất bại
        /// </summary>
        DeleteManyFalied = 26,

        /// <summary>
        /// Mã lỗi của mã nhân viên
        /// </summary>
        EmployeeCodeError = 27,
    }
}
