using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;

namespace SchoolManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetallStudent(int pageno,int pagesize,string? studename)
        {
            BaseResponse baseResponse= new BaseResponse();
            try
            {
                var result=await studentRepository.GetStudent(pageno, pagesize, studename);
                List<Student> students = (List<Student>)result.ResponseData;
                if(students.Count>0)
                {
                    baseResponse.ResponseData=students;
                    baseResponse.ResponseData1 = result.ResponseData1;
                }
                baseResponse.StatusMessage = "Data not fetched";
                return Ok(baseResponse);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpGet("GetStudentByid")]
        public async Task<IActionResult> GetStudentByid(int id)
        { 
        BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await studentRepository.GetStudeById(id);
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
        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent( Student student)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await studentRepository.AddStudent(student);
                if (res == null)
                {
                    baseResponse.StatusMessage = "Errror";
                }
                else
                {
                    baseResponse.StatusMessage = "Data Added Succesfully";

                   // baseResponse.ResponseData = res;
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await studentRepository.UpdateStudent(student);
                if (res == null)
                {
                    baseResponse.StatusMessage = "Errror";
                }
                else
                {
                    baseResponse.StatusMessage = "Data updated Succesfully";

                    // baseResponse.ResponseData = res;
                }
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteStudent")]
        public async Task<IActionResult> deleteStudent(int id )
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                var res = await studentRepository.DeleteStudent(id);
                if (res == null)
                {
                    baseResponse.StatusMessage = "Errror";
                }
                else
                {
                    baseResponse.StatusMessage = "Data Deleted Succesfully";

                    // baseResponse.ResponseData = res;
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
