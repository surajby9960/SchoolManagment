using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachController : ControllerBase
    {
        private readonly IConfiguration _con;
        private readonly ILogger logger;
        public ITeacherRepository _repo;

        public TeachController(IConfiguration con, ILoggerFactory logger, ITeacherRepository repo)
        {
            _con = con;
            this.logger = logger.CreateLogger<TeachController>();
            _repo = repo;
        }
        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher(Teacher data)
        {
            try
            {
                var res = await _repo.AddTeacher(data);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("GetTeacherByPagination")]
        public async Task<IActionResult> GetAllTeacherpage(int pageno,int pagesize,string? serach)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var response = await _repo.GetTeacherByPagination(pageno,pagesize,serach);
                if(response.ResponseData!=null)
                {
                    //List<ViewTeacher> viewTeachers = (List<ViewTeacher>)response.ResponseData1;
                    baseResponse.ResponseData = response.ResponseData;
                    baseResponse.ResponseData1 = response.ResponseData1;
                }
                else
                {
                    baseResponse.StatusMessage = "Error in fetching data";
                }
                return Ok(baseResponse);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
