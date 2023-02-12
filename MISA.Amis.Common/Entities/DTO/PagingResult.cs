namespace MISA.Amis.Common.Entities.DTO
{
    public class PagingResult
    {
        /// <summary>
        /// Tông số trang theo điều kiện
        /// </summary>
        /// Created By: Văn Anh (4/2/2023)
        public int? totalPage { get; set; }
        
        /// <summary>
        /// Tổng số bản ghi trên trang theo điều kiện
        /// </summary>
        /// Created By: Văn Anh (4/2/2023) 
        public int? totalRecord { get; set; }

        /// <summary>
        /// Dữ liệu nhân viên
        /// </summary>
        public List<Employee>? Data { get; set; }
    }
}
