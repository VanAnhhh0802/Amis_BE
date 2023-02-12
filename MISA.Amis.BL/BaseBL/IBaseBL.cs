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
        ServiceResult Update(T record);

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
        /// Hàm hiển thị thông tin record
        /// </summary>
        /// <param name="recordId">Id record cần hiển thị</param>
        /// <returns>
        /// 200: lấy ra thông tin thành công
        /// 500: lỗi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        T GetRecordById(Guid recordId);
    }
}
