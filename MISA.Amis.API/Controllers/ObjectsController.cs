using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.BL.AccountBL;
using MISA.Amis.BL.BaseBL;
using MISA.Amis.BL.ObjectBL;
using MISA.Amis.Common;
using MISA.Amis.Common.Entities;
using MISA.Amis.Common.Entities.DTO;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ObjectsController : BaseController<MObject>
    {
        #region Filed

        private IObjectBL _objectBL;

        #endregion
        #region Constructor
        public ObjectsController(IObjectBL objectBL) : base(objectBL)
        {
            _objectBL = objectBL;
        }
        #endregion

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            try
            {
                var allObject = _objectBL.GetObjectAll();
                // Xử lý kết quả trả về
                return StatusCode(StatusCodes.Status200OK, allObject);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        //protected IActionResult HandleException(Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
        //    {
        //        ErrorCode = Common.Enums.ErrorCode.Exception,
        //        DevMsg = Resource.DevMsg_Exception,
        //        UserMsg = Resource.UserMsg_Exception,
        //        MoreInfor = ex.Message,
        //        TranceId = HttpContext.TraceIdentifier
        //    });
        //}
    }
}
