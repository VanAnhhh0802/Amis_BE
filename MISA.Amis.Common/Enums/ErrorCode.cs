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
    }
}
