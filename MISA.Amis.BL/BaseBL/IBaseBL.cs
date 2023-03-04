using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Phân trang theo danh sách nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="employeeFilter">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentID">id của phòng ban</param>
        /// <returns>Danh sách nhân viên và số lượng bản ghi</returns>
        /// Created by: VĂn Anh (6/2/2023)
        PagingResult<T> GetRecordFilter(string keyword, Guid? departmentId,Guid? positionId, int pageSize,int pageNumber);

        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="T">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// 201: Thêm thành công
        /// 500: Lỗi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        ServiceResult Insert(T record);

        /// <summary>
        /// Hàm sửa record
        /// </summary>
        /// <param name="record">Đối tượng record cần sửa</param>
        /// <returns>
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        ServiceResult Update(T record, Guid id);

        /// <summary>
        /// Hàm xóa record
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần xóa</param>
        /// <returns>
        /// 200: Xóa thành công
        /// 500: Lỗi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        int Delete(Guid recordId);

        /// <summary>
        /// xóa nhiều bản ghi cùng 1 lúc 
        /// </summary>
        /// <param name="recordIds">Mảng id record cần xóa dưới dạnh chuỗi JSON</param>
        /// <returns>
        /// 200: Xóa thành công
        /// 500: Xóa thất bại
        /// </returns>
        ServiceResult DeleteMany(Guid[] recordIds);


        /// <summary>
        /// Hàm hiển thị thông tin record
        /// </summary>
        /// <param name="recordId">Id record cần hiển thị</param>
        /// <returns>
        /// 200: lấy ra thông tin thành công
        /// 500: lỗi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        T GetRecordById(Guid recordId);

        /// Hàm check mã record bị trùng
        /// </summary>
        /// <param name="recordCode">record</param>
        /// <param name="id">id record</param>
        /// <returns>true - mã record bị trùng, false - mã record không bị trùng</returns>
        public bool CheckDuplicate(T record, string recordCode, Guid id);

    }
}
