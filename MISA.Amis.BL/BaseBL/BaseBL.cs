using MISA.Amis.Common;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Enums;
using MISA.Amis.DL;
using MISA.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Amis.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion
        /// <summary>
        /// Hàm thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public ServiceResult Insert(T record)
        {
            //Validate đầu vào
            var validateErrorResults = ValidateRequestData(record);

            if (validateErrorResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                    Data = validateErrorResults,
                    Message = Resource.EmptyValue
                };
            }

            var numberOfAffectedRows = _baseDL.InsertRecord(record);
            // Xử lý kết quả trả về

            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InsertFailed,
                    Message = Resource.Message_InsertDLError
                };

            }
        }

        /// <summary>
        /// Hàm validate chung
        /// </summary>
        /// <param name="record"></param>
        /// <returns>List lỗi</returns>
        protected virtual List<string> ValidateRequestData(T? record)
        {
            //Validate đầu vào
            var result = ValidateCustom(record);
            //Khởi tạo danh sách lỗi
            List<string> listError = new List<string>();


            //Lấy ra danh sách các thuộc tính trong lớp Employee
            var properties = typeof(T).GetProperties();
            //Duyệt qua các phần tử trong properties
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(record);
                //Trường hợp property có value là Required
                var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    listError.Add(requiredAttribute.ErrorMessage);
                }

            }
            if (result.Count > 0)
            {
                for (int i = 0; i <= result.Count - 1; i++)
                {
                    listError.Add(result[i]);

                }
            }
            return listError;
        }

        /// <summary>
        /// Hàm validate các trường riêng của đối tượng
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        protected virtual List<string> ValidateCustom(T? record)
        {
            List<string> listErrorCustom = new List<string>();
            // Trường hợp Email không đúng định dạng
            var properties = typeof(T).GetProperties();
            //Duyệt qua các phần tử trong properties
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(record);
                var emailAttribute = (EmailAddressAttribute)(property.GetCustomAttributes(typeof(EmailAddressAttribute), false)).FirstOrDefault();
                if (emailAttribute != null && propertyValue != null)
                {
                    var regexEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    if (!Regex.IsMatch(propertyValue.ToString(), regexEmail))
                    {
                        listErrorCustom.Add(emailAttribute.ErrorMessage);
                    }
                }
            }
            return listErrorCustom;
        }

        /// <summary>
        /// Hàm sửa nhân viên
        /// </summary>
        /// <param name="record">Đối tượng nhân viên cần sửa</param>
        /// <returns>        
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public ServiceResult Update(T record)
        {
            //Validate đầu vào
            //Validate đầu vào
            var validateErrorResults = ValidateRequestData(record);

            if (validateErrorResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                    Data = validateErrorResults,
                    Message = Resource.EmptyValue
                };
            }

            var numberOfAffectedRows = _baseDL.UpdateRecord(record);
            // Xử lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.UpdateFailed,
                    Message = Resource.Message_UpdateDLError
                };

            }
        }

        /// <summary>
        /// Hàm xóa record
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần xóa</param>
        /// <returns>
        /// 200: Xóa thành công
        /// 500: Lỗi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public int Delete(Guid recordId)
        {
            var numberOfAffectedRows = _baseDL.DeleteRecord(recordId);
            // Xử lý kết quả trả về
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm hiển thị thông tin record
        /// </summary>
        /// <param name="recordId">Id record cần hiển thị</param>
        /// <returns>
        /// 200: Lấy ra thông tin bản ghi thành công
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public T GetRecordById(Guid recordId)
        {
            return _baseDL.GetRecordById(recordId);
        }
    }
}
