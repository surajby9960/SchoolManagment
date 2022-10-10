using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;

namespace SchoolManagment.Controllers
{
    [Route("SchoolManagment/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository classRepository;
        ILogger logger;
        public ClassController(IConfiguration configuration, IClassRepository classRepository, ILoggerFactory loggerFactory)
        {
                this.classRepository = classRepository;
               this.logger = loggerFactory.CreateLogger<ClassController>();
        }
        [HttpGet("/GetAllClass")]
        public async Task<IActionResult> GetAllClass()
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await classRepository.GetAllClasses();
                if (res == null)
                {
                    baseResponse.StatusMessage = "Data not found";
                }
                else
                {
                    baseResponse.StatusMessage = "Data fetched Succesfully";

                    baseResponse.ResponseData = res;
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetClassById")]
        public async Task<IActionResult> GetClassById( int id)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await classRepository.GetClassById(id);
                if (res == null)
                {
                    baseResponse.StatusMessage = "Data not found";
                }
                else
                {
                    baseResponse.StatusMessage = "Data fetched Succesfully";

                    baseResponse.ResponseData = res;
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass(InsertClass cls)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await classRepository.AddClass(cls);
                if (res == 0)
                {
                    baseResponse.StatusMessage = "Error";
                }
                else
                {
                    baseResponse.StatusMessage = "Data Added Succesfully";

                        
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateClass")]
        public async Task<IActionResult> UpdateClass(UpdateClass cls)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await classRepository.UpdateClass(cls);
                if (res == 0)
                {
                    baseResponse.StatusMessage = "Error";
                }
                else
                {
                    baseResponse.StatusMessage = "Data Updated Succesfully";


                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteClass")]
        public async Task<IActionResult> DeleteClass(int cid, int sid)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await classRepository.DeleteClass(cid,sid);
                if (res == 0)
                {
                    baseResponse.StatusMessage = "Error";
                }
                else
                {
                    baseResponse.StatusMessage = "Data Deleted Succesfully";


                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
