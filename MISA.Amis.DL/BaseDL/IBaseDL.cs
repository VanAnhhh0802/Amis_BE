using MISA.Amis.Common.Entities;
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
        /// Hàm thêm mới 1 bản ghi 
        /// </summary>
        /// <param name="record">Bản ghi 1 thêm</param>
        /// <returns>
        /// -201: insert thành công
        /// 500: insert thất bại
        /// </returns>
        /// created by: Văn Anh (8/2/2023)
        int InsertRecord(T record);
        /// <summary>
        /// Hàm sửa record
        /// </summary>
        /// <param name="record">Đối tượng record cần sửa</param>
        /// <returns>
        /// -200: sửa thành công
        /// -500: sửa thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        int UpdateRecord(T record);

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
        /// Hàm hiển thị thông tin record theo id
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần hiển thị</param>
        /// <returns>
        /// Trả về thông tin record theo id
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        T GetRecordById(Guid id);
    }
}
