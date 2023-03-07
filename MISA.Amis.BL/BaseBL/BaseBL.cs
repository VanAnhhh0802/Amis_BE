using Microsoft.AspNetCore.Http;
using MISA.Amis.Common;
using MISA.Amis.Common.CustomAttribute;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Enums;
using MISA.Amis.DL;
using MISA.Amis.DL.BaseDL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Amis.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        protected List<string> listErrorRequired = new List<string>();
        protected ServiceResult resultValidateCustom = new ServiceResult();
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Phân trang theo danh sách record
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="keyword">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentId">id của phòng ban</param>
        /// <param name="positionId">id của Chức vụ</param>
        /// <returns>Danh sách record và số lượng bản ghi theo điều kiện</returns>
        /// Created by: VĂn Anh (6/2/2023)
        public PagingResult<T> GetRecordFilter(string keyword, Guid? departmentId, Guid? positionId, int pageSize, int pageNumber)
        {
            return _baseDL.GetRecordFilter(keyword, departmentId, positionId, pageSize, pageNumber);
        }

        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public ServiceResult Insert(T record)
        {
            var newId = Guid.NewGuid();
            //Validate đầu vào
            var validateErrorResults = ValidateRequestData(record, newId);


            if (!validateErrorResults.IsSuccess)
            {
                return validateErrorResults;
            }
            var resultDL = _baseDL.InsertRecord(record, newId);
            // Xử lý kết quả trả về

            return new ServiceResult
            {
                IsSuccess = true,
                numberOfAffectedRows = resultDL,
            };
        }

        /// <summary>
        /// Hàm validate chung
        /// </summary>
        /// <param name="record"></param>
        /// <returns>List lỗi</returns>
        protected virtual ServiceResult ValidateRequestData(T? record, Guid id)
        {
            //Validate đầu vào cho những trường bắt buộc

            //Lấy ra danh sách các thuộc tính trong lớp record
            var properties = typeof(T).GetProperties();
            //Duyệt qua các phần tử trong properties
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(record);
                //Trường hợp property có value là Required
                var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                if (requiredAttribute != null && (string.IsNullOrEmpty(propertyValue.ToString()) 
                    || propertyValue.ToString() == "00000000-0000-0000-0000-000000000000"))
                {
                    listErrorRequired.Add(propertyName);
                }
                if (listErrorRequired.Count > 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                        Data = listErrorRequired,
                    };
                }

            }
            resultValidateCustom = ValidateCustom(record);


            if (resultValidateCustom.IsSuccess)
            {
                resultValidateCustom = ValidateCustom(record);
            }

            if (!resultValidateCustom.IsSuccess)
            {
                return resultValidateCustom;
            }
            else
            {

                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
        }

        /// <summary>
        /// Hàm validate các trường riêng của đối tượng
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        protected virtual ServiceResult ValidateCustom(T? record)
        {
            return new ServiceResult { };
        }


        /// <summary>
        /// Hàm sửa record
        /// </summary>
        /// <param name="record">Đối tượng record cần sửa</param>
        /// <returns>        
        /// Số bản ghi bị thay đổi
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public virtual ServiceResult Update(T record, Guid id)
        {
            //Validate đầu vào
            var validateErrorResults = ValidateRequestData(record, id);

            if (!validateErrorResults.IsSuccess)
            {
                return validateErrorResults;
            }

            var resultDL = _baseDL.UpdateRecord(id, record);
            // Xử lý kết quả trả về
            return new ServiceResult
            {
                IsSuccess = true,
                numberOfAffectedRows = resultDL
            };
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
        /// xóa nhiều bản ghi cùng 1 lúc 
        /// </summary>
        /// <param name="recordIds">Mảng id record cần xóa dưới dạnh chuỗi JSON</param>
        /// <returns>
        /// 200: Xóa thành công
        /// 500: Xóa thất bại
        /// </returns>
        public ServiceResult DeleteMany(Guid[] recordIds)
        {
            var jsonIds = JsonConvert.SerializeObject(recordIds);
            var result = _baseDL.DeleteMany(jsonIds);

            if (result > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true,
                    Data = result
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.DeleteManyError
                };
            }

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

        /// Hàm check mã record bị trùng
        /// </summary>
        /// <param name="recordCode">record</param>
        /// <returns>true - mã record bị trùng, false - mã record không bị trùng</returns>
        public bool CheckDuplicate(T record, string employeeCode, Guid id)
        {
            var result = _baseDL.CheckDuplicate(record, employeeCode, id);
            if (result > 0)
            {
                return true;
            }
            return false;
        }


        #endregion

    }
}
