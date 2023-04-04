using Dapper;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace MISA.Amis.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field
        public IDatabaseConnection DatabaseConnection;
        #endregion

        #region Constructor
        public BaseDL(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phân trang theo danh sách nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang thỏa mãn điều kiện</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="employeeFilter">Tìm theo mã, tên, số điện thoại </param>
        /// <param name="departmentId">id của phòng ban</param>
        /// <returns>Danh sách nhân viên và số lượng bản ghi</returns>
        /// Created by: VĂn Anh (6/2/2023)
        public PagingResult<T> GetRecordFilter(string? keyword, Guid? departmentId, Guid? positionId, int pageSize, int pageNumber)
        {
            string storedProcedureName = String.Format(ProcedureName.GetFilter, typeof(T).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_keyword", keyword);
            // Chuẩn bị tham số đầu vào cho stored
            GridReader result;
            var data = new PagingResult<T>();
            //Khởi tạo kết nốt tới DB
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.QueryMultiple(
                  storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                var listData = result.Read<T>().ToList();

                var totalRecords = result.ReadFirstOrDefault<int>();

                data = new PagingResult<T>()
                {                                                                                   
                    totalRecord = totalRecords,
                    totalPage = (totalRecords % pageSize) == 0 ? (totalRecords / pageSize) : (totalRecords / pageSize) + 1,
                    Data = listData,
                };
            }
            return data;
        }

        /// <summary>
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// -201: insert thành công
        /// -500: insert thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public Guid InsertRecord(T record, Guid newId)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(T).Name);

            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            GeneratePropertyValue(record, properties, parameters);
            GeneratePrimaryKeyValue(properties, parameters, newId);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            int numberOfAffectedRows;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                numberOfAffectedRows = mySqlConnection.Execute(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);
            }

            if (numberOfAffectedRows > 0)
            {
                return newId;
            }
            //kết quả trả về
            return Guid.Empty;
        }
       
        /// <summary>
        /// Hàm lấy các value có trong record
        /// </summary>
        /// <param name="record">đối tượng cần lấy</param>
        /// <param name="properties">Tất cả các thuộc tính trong đối tượng</param>
        /// <param name="parameters">tham số cần truyền</param>
        /// Created by: VĂn Anh (6/2/2023)
        protected virtual void GeneratePropertyValue(T record, PropertyInfo[] properties, DynamicParameters parameters)
        {
            foreach (var property in properties)
            {
                parameters.Add($"p_{property.Name}", property.GetValue(record));
            }
        }

        /// <summary>
        /// Hàm lấy ra các giá trị là khóa chính của đối tượng
        /// </summary>
        /// <param name="properties">Tất cả các thuộc tính có trong đối tượng</param>
        /// <param name="parameters">Tham số</param>
        /// Created by: VĂn Anh (6/2/2023)
        protected virtual void GeneratePrimaryKeyValue(PropertyInfo[] properties, DynamicParameters parameters, Guid id)
        {
            foreach (var property in properties)
            {
                var keyAttribute = (KeyAttribute?)property.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if (keyAttribute != null)
                {
                    parameters.Add($"p_{property.Name}", id);
                }
            }
        }

        /// <summary>
        /// Hàm sửa đối tượng record
        /// </summary>
        /// <param name="record">đối tượng cần sửa</param>
        /// <returns>
        /// 200: sửa thành công
        /// 500: Thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public Guid UpdateRecord(Guid id, T record)
        {
            string storedProcedureName = String.Format(ProcedureName.Update, typeof(T).Name);

            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            GeneratePropertyValue(record, properties, parameters);
            GeneratePrimaryKeyValue(properties, parameters, id);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            int numberOfAffectedRows;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                numberOfAffectedRows = mySqlConnection.Execute(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return id;
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Hàm xóa record
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần xóa</param>
        /// <returns>
        /// -200: xóa thành công
        /// -500: xóa thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public int DeleteRecord(Guid recordId)
        {
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(T).Name);

            var parameters = new DynamicParameters();

            parameters.Add($"p_{typeof(T).Name}Id", recordId);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            var numberOfAffectedRows = 0;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                numberOfAffectedRows = mySqlConnection.Execute(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);

            }
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
        public int DeleteMany(string recordIds)
        {
            string storedProcedureName = String.Format(ProcedureName.DeleteMany, typeof(T).Name);

            var parameters = new DynamicParameters();

            parameters.Add($"p_{typeof(T).Name}Ids", recordIds);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            var numberOfAffectedRows = 0;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                using (var trasaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        //Gọi vào Db
                        numberOfAffectedRows = mySqlConnection.Execute(
                           storedProcedureName,
                           parameters,
                           trasaction,
                           commandType: System.Data.CommandType.StoredProcedure);
                        trasaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        trasaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }

                }
            }
            // Xử lý kết quả trả về
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm hiển thị thông tin record theo id
        /// </summary>
        /// <param name="recordId">Id của đối tượng record cần hiển thị</param>
        /// <returns>
        /// 200: thành công
        /// 500: Thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public T GetRecordById(Guid recordId)
        {
            string storedProcedureName = String.Format(ProcedureName.GetById, typeof(T).Name);

            var parameters = new DynamicParameters();

            parameters.Add($"p_{typeof(T).Name}Id", recordId);
            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            dynamic result;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                mySqlConnection.Open();
                //Gọi vào Db
                result = mySqlConnection.QueryFirstOrDefault<T>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure);
            }
            // Xử lý kết quả trả về
            return result;
        }

        /// Hàm check mã record bị trùng
        /// </summary>
        /// <param name="recordCode">mã record</param>
        /// <param name="id">id record</param>
        /// <returns>true - mã record bị trùng, false - mã record không bị trùng</returns>
        public int CheckDuplicate(T record, string recordCode, Guid id)
        {
            string storedProcedureName = String.Format(ProcedureName.CheckDuplicateCode, typeof(T).Name);

            var parameters = new DynamicParameters();
            parameters.Add($"p_{typeof(T).Name}Code", recordCode);
            parameters.Add($"p_{typeof(T).Name}Id", id);

            // Chuẩn bị tham số đầu vào cho stored
            //Khởi tạo kết nốt tới DB
            var result = 0;
            using (var mySqlConnection = DatabaseConnection.ConnectDatabase())
            {
                //Gọi vào Db
                result = mySqlConnection.Query<int>(
                   storedProcedureName,
                   parameters,
                   commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
            // Xử lý kết quả trả về
        }
        #endregion
    }
}
