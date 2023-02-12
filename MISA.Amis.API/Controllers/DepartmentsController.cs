using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Common.Entities;

namespace MISA.Amis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {

        /// <summary>
        /// Tạo danh sách các phòng ban
        /// </summary>
        /// Created BY: Văn Anh (17/1/2023)
        public List<Department> Departments = new List<Department>()
        {
            new Department{
                DepartmentId = Guid.Parse("3f8e6896-4c7d-15f5-a018-75d8bd200d7c"),
                DepartmentName = "Phòng Công Nghệ Thông Tin",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon",
                CreatedDate= DateTime.Now,
                ModifiedDate= DateTime.Now,
            },
            new Department{
                DepartmentId = Guid.Parse("9af69954-847f-4766-af7c-446c77e490f8"),
                DepartmentName = "Phòng Hành Chính",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon",
                CreatedDate= DateTime.Now,
                ModifiedDate= DateTime.Now,
            },
            new Department{
                DepartmentId = Guid.Parse("c883e7fc-510f-493a-9f2a-5dd8b8e2b295"),
                DepartmentName = "Phòng Nhân Sự",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon",
                CreatedDate= DateTime.Now,
                ModifiedDate= DateTime.Now,
            },
            new Department{
                DepartmentId = Guid.Parse("8d2350f0-06e3-4cc1-bce5-39c021e91523"),
                DepartmentName = "Phòng Kế toán",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon",
                CreatedDate= DateTime.Now,
                ModifiedDate= DateTime.Now,
            },
        };

        /// <summary>
        /// API lấy danh sách phòng ban
        /// </summary>
        /// <returns></returns>
        /// Created BY: Văn Anh (17/1/2023)
        [HttpGet]
        public IActionResult GetDepartments()
        {

            return StatusCode(StatusCodes.Status200OK, Departments);
        }
    }
}
