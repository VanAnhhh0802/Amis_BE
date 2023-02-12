using MISA.Amis.Common.Enums;

namespace MISA.Amis.Common.Entities.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi trả về
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Message lỗi về cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message lỗi về cho uer
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Danh sách chứa các mã lỗi
        /// </summary>
        public object MoreInfor { get; set; }

        /// <summary>
        /// Id mã lỗi
        /// </summary>
        public string TranceId { get; set; }
    }
}
