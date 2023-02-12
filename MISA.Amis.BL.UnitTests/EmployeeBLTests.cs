using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.BL.UnitTests
{
    //[Test]
    public class EmployeeBLTests
    {
        public void InsertRecord_EmployeeCode_ReturnsInvalid()
        {
            //Arrangge
            var employee = new Employee
            {
                EmployeeCode = String.Empty,
                EmployeeName = "Hồ Văn Anh",
                Gender = Enums.Gender.Male
            };
            var expectedResult = new ValidateResult
            {

            };
            //Act

            //Assert
        }
    }
}
