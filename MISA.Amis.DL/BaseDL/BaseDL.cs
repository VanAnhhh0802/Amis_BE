using Dapper;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        /// Hàm thêm mới record
        /// </summary>
        /// <param name="record">Đối tượng record cần thêm mới</param>
        /// <returns>
        /// -201: insert thành công
        /// -500: insert thất bại
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public int InsertRecord(T record)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(T).Name);

            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            GeneratePropertyValue(record, properties, parameters);
            GeneratePrimaryKeyValue(properties, parameters);
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
            //kết quả trả về
            return numberOfAffectedRows;
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
        protected virtual void GeneratePrimaryKeyValue(PropertyInfo[] properties, DynamicParameters parameters)
        {
            foreach (var property in properties)
            {
                var keyAttribute = (KeyAttribute?)property.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if (keyAttribute != null)
                {
                    parameters.Add($"p_{property.Name}", Guid.NewGuid());
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
        public int UpdateRecord(T record)
        {
            string storedProcedureName = String.Format(ProcedureName.Update, typeof(T).Name);

            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            GeneratePropertyValue(record, properties, parameters);
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
            return numberOfAffectedRows;
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
        #endregion
    }
}
