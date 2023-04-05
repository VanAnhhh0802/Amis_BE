using Aspose.Cells;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Constant;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Entities.MPayment;
using MISA.Amis.Common.Enums;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.EmployeeDL;
using MISA.Amis.DL.PaymentDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace MISA.Amis.PaymentBL
{
       
    public class PaymentBL : BaseBL<Payment>, IPaymentBL
    {
        #region Field

        private IPaymentDL _paymentDL;

        #endregion

        #region Constructor

        public PaymentBL(IPaymentDL paymentDL) : base(paymentDL)
        {
            _paymentDL = paymentDL;
        }
        #endregion

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// Mã nhân viên mới
        /// </returns>
        /// Created by: VĂn Anh (6/2/2023)
        public string GetNewPaymentNumber()
        {
            var newNumber = _paymentDL.GetNewPaymentNumber();
            return newNumber;
        }

        /// <summary>
        /// Hàm xuất danh sách chứng từ thành file excel
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <returns>File danh sách Chứng từ</returns>
        /// Creatd By: Văn Anh (17/2/2023)
        public MemoryStream ExportPayment(string keyword)
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
                var data = _paymentDL.ExportPayment(keyword);

                //Hợp phạm vi vào 1 ô duy nhất
                limit.Merge();
                //Try cập vào ô đầu tiên của trang tính
                var cell = dataTableWorkSheet.Cells["A1"];
                //Đặt tiêu đề cho file excel
                cell.Value = "DANH SÁCH CHỨNG TỪ";
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
                //Áp dụng trong phạm vi A1 tới I1 đều đc sử dụng
                limit.ApplyStyle(style, flag);

                //Hợp nhất 2 hàng
                //Tạo 1 phạm vi 
                limit = dataTableWorkSheet.Cells.CreateRange("A2:I2");
                //Căn chỉnh độ rộng cho hàng và cột trong excel
                dataTableWorkSheet.AutoFitColumns();
                dataTableWorkSheet.Cells.SetRowHeight(2, 16);
                dataTableWorkSheet.Cells.SetColumnWidth(0,255);
                dataTableWorkSheet.Cells.SetColumnWidth(1, 255);
                dataTableWorkSheet.Cells.SetColumnWidth(2, 255);
                dataTableWorkSheet.Cells.SetColumnWidth(3, 255);
                dataTableWorkSheet.Cells.SetColumnWidth(4, 225);
                dataTableWorkSheet.Cells.SetColumnWidth(5, 225);
                dataTableWorkSheet.Cells.SetColumnWidth(6, 225);
                dataTableWorkSheet.Cells.SetColumnWidth(7, 225);
                dataTableWorkSheet.Cells.SetColumnWidth(8, 225);


                limit.Merge();
                //Áp dụng trong phạm vi
                limit.ApplyStyle(style, flag);

                var paymentTable = new DataTable("Payment");

                //Thêm các cột trong tương ứng với các trường cần export trong Payment
                paymentTable.Columns.Add(ExcelColumsName.INDEX, typeof(long));
                paymentTable.Columns.Add(ExcelColumsName.PAYMENT_DATE, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.POSTED_DATE, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.PAYMENT_NUMBER, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.REASON, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.TOTAL_AMOUN, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.OJECT_CODE, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.OJECT_NAME, typeof(string));
                paymentTable.Columns.Add(ExcelColumsName.ADDRESS, typeof(string));

                var index = 1;
                foreach (var item in data)
                {
                    //Tạo 1 hàng mới để thêm vào bảng dữ liệu
                    var record = paymentTable.NewRow();

                    //đặt dữ liệu cho từng hàng
                    record[ExcelColumsName.INDEX] = index;
                    record[ExcelColumsName.PAYMENT_DATE] = item.PaymentDate == null ? "" : ((DateTime)item.PaymentDate).ToString("dd/MM/yyyy");
                    record[ExcelColumsName.POSTED_DATE] = item.PostedDate == null ? "" : ((DateTime)item.PostedDate).ToString("dd/MM/yyyy");
                    record[ExcelColumsName.PAYMENT_NUMBER] = item.PaymentNumber;
                    record[ExcelColumsName.REASON] = item.Reason;
                    record[ExcelColumsName.TOTAL_AMOUN] =  item.TotalAmount;
                    record[ExcelColumsName.OJECT_CODE] = item.ObjectCode;
                    record[ExcelColumsName.OJECT_NAME] = item.ObjectName;
                    record[ExcelColumsName.ADDRESS] = item.Address;

                    paymentTable.Rows.Add(record);
                    index++;
                }
                //Khởi tạo một đối tượng của ImportTableOptions để kiểm soát việc nhập DataTable vào Excel
                var importTableOption = new ImportTableOptions();

                dataTableWorkSheet.Cells.ImportData(paymentTable, 2, 0, importTableOption);
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
        /// Hàm format tiền dùng cho xuất excel
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string FormatMoney(decimal? money)
        {
            try
            {
               string result = String.Format("{0:0,0}", money);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

       
        /// <summary>
        /// Hàm validate riêng cho phiếu chi
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override ServiceResult ValidateCustom(Payment? payment, Guid id)
        {
            //check mã nhân viên 
            if (payment.PaymentNumber != null && !string.IsNullOrEmpty(payment.PaymentNumber?.ToString()))
            {
                //Check mã trùng
                var duplicateCode = CheckDuplicate(payment, (string)payment.PaymentNumber, id);
                if (duplicateCode)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = Common.Enums.ErrorCode.DuplicateCode,
                        Message = "Số phiêu chi <" + payment.PaymentNumber.ToString() + "> bị trùng"
                    };
                }
            }

            //Check độ dài vượt quá
            if (payment?.PaymentNumber != null && !string.IsNullOrEmpty(payment.PaymentNumber?.ToString()))
            {
                var lengthNumber = ((string)payment.PaymentNumber).Length;
                if (lengthNumber > 20)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.OutMaxLengthCode,
                    };
                }
            }

            //Check Ngày hạch toán lơn hơn ngày chứng từ
            if (payment.PostedDate != null && !string.IsNullOrEmpty(payment.PostedDate?.ToString()) || payment.PaymentDate != null && !string.IsNullOrEmpty(payment.PaymentDate?.ToString()))
            {
                var paymentDate = (DateTime?)payment.PaymentDate;
                var postedDate = (DateTime?)payment.PostedDate;
                if (postedDate  > paymentDate) {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        ErrorCode = ErrorCode.DateValid,
                        Message = Resource.PaymentDateError
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
