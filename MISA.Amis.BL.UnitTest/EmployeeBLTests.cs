using MISA.Amis.BL;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;
using MISA.Amis.Common.Enums;
using MISA.Amis.DL.BaseDL;
using MISA.Amis.DL.EmployeeDL;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.UnitTest
{
    public class EmployeeBLTests
    {
        #region Filed
        private IEmployeeBL _employeeBL;
        private IEmployeeDL _fakeEmployeeDL;
        #endregion
        [SetUp]
        public void SetUp()
        {
            _fakeEmployeeDL = Substitute.For<IEmployeeDL>();
            _employeeBL = new EmployeeBL(_fakeEmployeeDL);
        }

        /// <summary>
        /// Hàm test trường EmployeeCode để trống
        /// </summary>
        /// created by: Văn Anh (11/2/2023)
        [Test]
        public void InsertRecord_Valid_ReturnsInvalid()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = String.Empty,
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                Data = new List<string>
                {
                    "EmployeeCode"
                },

            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);
            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        /// <summary>
        /// Hàm test trường EmployeeCode để trống
        /// </summary>
        /// created by: Văn Anh (11/2/2023)
        [Test]
        public void InsertRecord_EmployeeCode_ReturnsInvalid()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = String.Empty,
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                Data = new List<string>
                {
                    "EmployeeCode"
                },

            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);
            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        /// <summary>
        /// Hàm test trường F để FullName trống
        /// </summary>
        /// created by: Văn Anh (11/2/2023)
        [Test]
        public void InsertRecord_FullName_ReturnsInvalid()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),

                EmployeeCode = "NV-0001",
                FullName = String.Empty,
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                Data = new List<string>
                {
                    "FullName"
                },
                Message = Resource.EmptyValue
            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);
            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        /// <summary>
        /// Hàm test trường department không được để trống
        /// </summary>
        [Test]
        public void InsertRecord_Department_ReturnsInvalid()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),

                EmployeeCode = "NV-0001",
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Empty,
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.EmptyValue,
                Data = new List<string>()
                {
                    "DepartmentId"
                },
                Message = Resource.EmptyValue
            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);
            Assert.AreEqual(expectedResult.Data, actualResult.Data);
        }

        /// <summary>
        /// Hàm test Email không đúng định dạng
        /// </summary>
        [Test]
        public void InsertRecord_EmployeeEmail_ReturnsInvalid()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeCode = "NV-0001",
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.EmailInValidate,
            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);
        }

        /// <summary>
        /// Hàm test OutLength trả về bị lỗi
        /// TH: 1 là thành công
        /// </summary>
        [Test]
        public void InsertRecord_OutLengthProperty_ReturnsValidResultSuccess()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV-0000000000000000000000000000000000000000000000000000000000000000000001",
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            //_fakeEmployeeDL.InsertRecord(employee).Returns(1);

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = ErrorCode.OutMaxLengthCode,
            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);


        }

        /// <summary>
        /// Hàm test ngày không được lớn hơn ngày hiện tại
        /// TH: 1 là thành công
        /// </summary>
        [Test]
        public void InsertRecord_DateNotBigerCurrentDate_ReturnsValidResultSuccess()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV-1",
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = new DateTime(2030, 12, 31),
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };

            //_fakeEmployeeDL.InsertRecord(employee).Returns(1);

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = ErrorCode.DateValid,
            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);


        }

        [Test]
        public void InsertRecord_ValidSuccess_ReturnValidSucces()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV-1000",
                FullName = "Hồ Văn Anh",
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.Parse("17120d02-6ab5-3e43-18cb-66948daf6128"),
                DateOfBirth = DateTime.Now,
                PositionName = "Giám đốc",
                IdentityNumber = "032155458",
                IdentityDate = DateTime.Now,
                IdentityPlace = "Thanh Hóa",
                Address = "Hà Nội",
                PhoneNumber = "0145874",
                TelephoneNumber = "01555511",
                Email = "Anh@example.com",
                BankAccountNumber = "054664678",
                BankName = "ACb",
                BankBranch = "Hà Thành",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Văn Anh",
                ModifiedBy = "Nguyễn Văn Bình",
                ModifiedDate = DateTime.Now
            };


            var expectedResult = new ServiceResult
            {
                IsSuccess = true,
            };
            //Act
            var actualResult = _employeeBL.Insert(employee);

            //Assert
            Assert.AreEqual(expectedResult.IsSuccess, actualResult.IsSuccess);

        }
    }
}
