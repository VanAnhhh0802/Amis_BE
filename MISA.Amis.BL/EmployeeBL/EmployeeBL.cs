using Dapper;
using MISA.Amis.Common.Enums;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.EmployeeDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using Aspose.Cells;
using System.Data;
using MISA.Amis.Common.Constant;
using ServiceStack;
using MISA.Amis.Common.CustomAttribute;
using System.Resources;

namespace MISA.Amis.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public string GetNewEmployeeCode()
        {
            var maxEmployeeCode = _employeeDL.GetNewEmployeeCode();
            string maxEmployeeCodeNumber = Regex.Replace(maxEmployeeCode, "[^0-9]", "");
            string maxEmployeeCodeString = Regex.Replace(maxEmployeeCode, "[0-9]", "");

            string newEmployeeCodeNumber = long.Parse(maxEmployeeCodeNumber) + 1 + "";

            string newEmployeeCode = maxEmployeeCodeString + newEmployeeCodeNumber;
            return newEmployeeCode;
        }

        /// <summary>
        /// Hàm xuất danh sách nhân viên thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách nhân viên</returns>
        /// Creatd By: Văn Anh (17/2/2023)
        public MemoryStream ExportEmployee(string keyword)
        {
            //Tạo đối tượng workbook để xuất datatable
            var memoryStream = new MemoryStream();
            var workbookForDataTable = new Workbook(memoryStream);
            //Lấy tham chiếu đến ô đầu tiên trong workbook
            var dataTableWorkSheet = workbookForDataTable.Worksheets[0];
            //Tạo phạm vi của bảng
            var limit = dataTableWorkSheet.Cells.CreateRange("A1:I1");

            try
            {
                //Lấy dữ liệu từ DL
                var data = _employeeDL.ExportEmployee(keyword);

                //Hợp phạm vi vào 1 ô duy nhất
                limit.Merge();
                //Try cập vào ô đầu tiên của trang tính
                var cell = dataTableWorkSheet.Cells["A1"];
                //Đặt tiêu đề cho file excel
                cell.Value = "DANH SÁCH NHÂN VIÊN";
                //Xác định 1 đối tượng phong cách
                var style = workbookForDataTable.CreateStyle();
                //Đặt căn chỉnh
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.IsBold = true;
                style.Font.Size = 16;
                style.Font.Name = "Times New Roman";

                //Tạo 1 đối tượng styleFlag
                var flag = new StyleFlag();
                //Bật thuộc tính kiểu dáng tương đối
                flag.All = true;
                //Áp dụng trong phạm vi
                limit.ApplyStyle(style, flag);

                //Hợp nhất 2 hàng
                //Tạo 1 phạm vi 
                limit = dataTableWorkSheet.Cells.CreateRange("A2:I2");
                dataTableWorkSheet.AutoFitColumns();
                dataTableWorkSheet.Cells.SetRowHeight(2, 16);
                dataTableWorkSheet.Cells.SetColumnWidth(0, 200);
                dataTableWorkSheet.Cells.SetColumnWidth(1, 200);
                dataTableWorkSheet.Cells.SetColumnWidth(2, 200);
                dataTableWorkSheet.Cells.SetColumnWidth(3, 200);
                dataTableWorkSheet.Cells.SetColumnWidth(4, 200);

                limit.Merge();
                //Áp dụng trong phạm vi
                limit.ApplyStyle(style, flag);

                var employeeTable = new DataTable("Employee");

                //Thêm các cột trong tương ứng với các trường cần export trong Employee
                employeeTable.Columns.Add(ExcelColumsName.INDEX, typeof(long));
                employeeTable.Columns.Add(ExcelColumsName.EMPLOYEE_CODE, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.EMPLOYEE_NAME, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.GENDER, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.DATE_OF_BIRTH, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.DEPARTMENT, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.POSITION, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.BANK_ACCOUNT, typeof(string));
                employeeTable.Columns.Add(ExcelColumsName.BANK_NAME, typeof(string));

                var index = 1;
                foreach (var item in data)
                {
                    //Tạo 1 hàng mới để thêm vào bảng dữ liệu
                    var record = employeeTable.NewRow();

                    //đặt dữ liệu cho từng hàng
                    record[ExcelColumsName.INDEX] = index;
                    record[ExcelColumsName.EMPLOYEE_CODE] = item.EmployeeCode;
                    record[ExcelColumsName.EMPLOYEE_NAME] = item.FullName;
                    record[ExcelColumsName.GENDER] = FormatGender(item.Gender);
                    record[ExcelColumsName.DATE_OF_BIRTH] = item.DateOfBirth == null ? "" : ((DateTime)item.DateOfBirth).ToString("dd/MM/yyyy");
                    record[ExcelColumsName.DEPARTMENT] = item.DepartmentName;
                    record[ExcelColumsName.POSITION] = item.PositionName;
                    record[ExcelColumsName.BANK_ACCOUNT] = item.BankAccountNumber;
                    record[ExcelColumsName.BANK_NAME] = item.BankName;

                    employeeTable.Rows.Add(record);
                    index++;
                }
                //Khởi tạo một đối tượng của ImportTableOptions để kiểm soát việc nhập DataTable vào Excel
                var importTableOption = new ImportTableOptions();

                dataTableWorkSheet.Cells.ImportData(employeeTable, 2, 0, importTableOption);
                //Tự động dãn dòng trong excel
                dataTableWorkSheet.AutoFitColumns();

                // Style cho tiêu đề các cột
                // Tạo một phạm vi
                limit = dataTableWorkSheet.Cells.CreateRange("A3:I3");

                // Đặt căn chỉnh.
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.IsBold = true;
                style.Font.Size = 12;
                style.Font.Name = "Times New Roman";

                style.Pattern = BackgroundType.Solid;
                style.ForegroundColor = System.Drawing.Color.LightGray;
                // Áp style
                limit.ApplyStyle(style, flag);

                //style cho nội dung
                // Tạo một phạm vi
                limit = dataTableWorkSheet.Cells.CreateRange(3, 0, data.Count, 9);
                // Đặt căn chỉnh.
                style.HorizontalAlignment = TextAlignmentType.Left;
                style.Font.IsBold = false;
                style.Font.Size = 12;
                style.Font.Name = "Times New Roman";
                style.Pattern = BackgroundType.None;
                style.ForegroundColor = System.Drawing.Color.Empty;
                // Áp style
                limit.ApplyStyle(style, flag);

                //Lưu file Excel
                workbookForDataTable.Save(memoryStream, SaveFormat.Xlsx);
                memoryStream.Position = 0;
                return memoryStream;
            }
            finally
            {
                workbookForDataTable.Dispose();
                memoryStream.Dispose();
                dataTableWorkSheet.Dispose();
            }
        }

        /// <summary>
        /// Format giới tính để export
        /// </summary>
        /// <param name="gender">giới tính</param>
        /// <returns>Dạng chữ của giới tính</returns>
        private static string FormatGender(Gender? gender)
        {
            try
            {
                switch (gender)
                {
                    case Gender.Male:
                        return Resource.Male;

                    case Gender.Female: return Resource.FeMale;

                    case Gender.Other: return Resource.Other;

                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// Hàm validate custom những trường hợp riêng cho employee
        /// </summary>
        /// <param name="employee">đối tượng employee</param>
        /// <returns>đối tượng service result</returns>
        /// Created by: Văn Anh (4/3/2023)
        protected override ServiceResult ValidateCustom(Employee? employee)
        {
            //check mã nhân viên 
            if (employee.EmployeeCode != null && !string.IsNullOrEmpty(employee.EmployeeCode?.ToString()))
            {
                //Check mã trùng
                var duplicateCode = CheckDuplicate(employee, (string)employee.EmployeeCode, employee.EmployeeId);
                if (duplicateCode)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = Common.Enums.ErrorCode.DuplicateCode,
                    };
                }
            }
            //Email  không đúng định dạng
            if (employee?.Email != null && !string.IsNullOrEmpty(employee.Email?.ToString()))
            {
                var regexEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                if (!Regex.IsMatch(employee.Email.ToString(), regexEmail))
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = Common.Enums.ErrorCode.EmailInValidate,
                    };
                }
            }
            //Check độ dài vượt quá
            if (employee?.EmployeeCode != null && !string.IsNullOrEmpty(employee.EmployeeCode?.ToString()) || employee?.FullName != null && !string.IsNullOrEmpty(employee.FullName?.ToString()))
            {
                var lengthName = ((string)employee.FullName).Length;
                var lengthCode = ((string)employee.EmployeeCode).Length;
                if (lengthCode > 20)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.OutMaxLengthCode,
                    };
                }
                if (lengthName > 100)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.OutMaxLengthName,
                    };
                }
            }
            //Check ngày Không được lớn hơn ngày hiện tại
            if (employee.DateOfBirth != null && !string.IsNullOrEmpty(employee.DateOfBirth?.ToString()) || employee.IdentityDate != null && !string.IsNullOrEmpty(employee.IdentityDate?.ToString()))
            {
                var dateOfBirth = (DateTime)employee.DateOfBirth;
                var identityDate = (DateTime)employee.IdentityDate;
                if (dateOfBirth > DateTime.Now)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.DateValid,
                    };
                }
                if (identityDate > DateTime.Now)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.DateValid,
                    };
                }
            }
            return new ServiceResult
            {
                IsSuccess = true,
            };
        }
    }
}
