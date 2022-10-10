using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;

namespace SchoolManagment.Controllers
{
    [Route("SchoolManagement/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolrepository _repo;
        private readonly ILogger logger;
        public SchoolController(IConfiguration configuration,ISchoolrepository schoolrepository,ILoggerFactory loggerFactory)
        {
            _repo = schoolrepository; 
                logger=loggerFactory.CreateLogger<SchoolController>();
        }

        [HttpPost("AddSchool")]
        public async Task<IActionResult>AddSchool(School school)
        {
            BaseResponse baseResponse=new BaseResponse();
            logger.LogDebug(string.Format("SchoolController:AddSchool"));
            try
            {
                var res = await _repo.AddSchool(school);
                if (res == 0)
                {
                    baseResponse.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    baseResponse.StatusMessage = "Error while Adding";
                    
                }
                else
                {
                    baseResponse.StatusCode = StatusCodes.Status201Created.ToString();
                    baseResponse.StatusMessage = "Added Succeesfully";
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpGet("GetAllSchool")]
       /* public async Task<IActionResult> GetAllSchool()
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res=await _repo.GetSchool();
                if(res==null)
                {
                    baseResponse.StatusMessage = "Data not found";
                }
                else
                {
                    baseResponse.StatusMessage = "Data fetched Succesfully";
                    
                    baseResponse.ResponseData = res;
                }
                return Ok(baseResponse);
            } catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }*/
        [HttpGet("GetAllSchoolById")]
        public async Task<IActionResult> GetAllSchoolById(int id )
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await _repo.GetSchoolById(id);
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
        [HttpDelete("DeleteSchool")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await _repo.DeleteSchool(id);
                if (res == 0)
                {
                    baseResponse.StatusMessage = "Error";
                }
                else
                {
                    baseResponse.StatusMessage = "Delte Succesfully";
                    baseResponse.ResponseData = res;
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateSchool")]
        public async Task<IActionResult> UpdateSchool(School school)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await _repo.UpdateSchool(school);
                if (res == 0)
                {
                    baseResponse.StatusMessage = "Error";
                }
                else
                {
                    baseResponse.StatusMessage = "Update Succesfully";
                  
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> ByPagination(int pageno,int pagesize,string? schoolname)
        {
            try
            {
                var response = await _repo.GetSchool(pageno, pagesize, schoolname);
                List<School> schools = new List<School>();
                schools.Add((School)response.ResponseData1);
                return Ok(schools);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
