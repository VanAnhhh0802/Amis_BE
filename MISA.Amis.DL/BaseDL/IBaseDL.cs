using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.Amis.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Phân trang theo danh sách nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="employeeFilter">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentId">id của phòng ban</param>
        /// <returns>Danh sách nhân viên và số lượng bản ghi</returns>
        /// Created by: VĂn Anh (6/2/2023)
        PagingResult<T> GetRecordFilter (string keyword, Guid? departmentId, Guid? positionId, int pageSize, int pageNumber);

        /// <summary>
        /// Hàm thêm mới 1 bản ghi 
        /// </summary>
        /// <param name="record">Bản ghi 1 thêm</param>
        /// <returns>
        /// -201: insert thành công
        /// 500: insert thất bại
        /// </returns>
        /// created by: Văn Anh (8/2/2023)
        int InsertRecord(T record, Guid newId);
        /// <summary>
        /// Hàm sửa record
        /// </summary>
        /// <param name="record">Đối tượng record cần sửa</param>
        /// <returns>
        /// -200: sửa thành công
        /// -500: sửa thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        int UpdateRecord(Guid id, T record);

        /// <summary>
        /// Hàm xóa record
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần xóa</param>
        /// <returns>
        /// -200: xóa thành công
        /// -500: xóa thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        int DeleteRecord(Guid recordId);

        /// <summary>
        /// xóa nhiều bản ghi cùng 1 lúc 
        /// </summary>
        /// <param name="recordIds">Mảng id record cần xóa dưới dạnh chuỗi JSON</param>
        /// <returns>
        /// 200: Xóa thành công
        /// 500: Xóa thất bại
        /// </returns>
        int DeleteMany(string recordIds);

        /// <summary>
        /// Hàm hiển thị thông tin record theo id
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần hiển thị</param>
        /// <returns>
        /// Trả về thông tin record theo id
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        T GetRecordById(Guid id);

        /// <summary>
        /// Hàm check mã record bị trùng
        /// </summary>
        /// <param name="recordCode">Mã record</param>
        /// <param name="id">id record</param>
        /// <returns>true - mã record bị trùng, false - mã record không bị trùng</returns>
        public int CheckDuplicate(T record, string recordCode, Guid id);
    }
}
