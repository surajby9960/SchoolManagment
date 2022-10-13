using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;

namespace SchoolManagment.Controllers
{
    [Route("SchoolManagement/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ISportsRepository sportsRepository;
        public SportsController(ISportsRepository sportsRepository)
        {
            this.sportsRepository = sportsRepository;
        }
        [HttpGet("GetAllsports")]
        public async Task<IActionResult> GetAllSport()
        {
            try
            {
                var sports=await sportsRepository.GetAllSport();
                return Ok(sports);
            }catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpGet("GetsportById")]
        public async Task<IActionResult> GetsportById(int id)
        {
            try
            {
                var sports = await sportsRepository.GetSportById(id);
                return Ok(sports);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost("AddSports")]
        public async Task<IActionResult> AddSports(Sports sports )
        {
            try
            {
                var res = await sportsRepository.AddSport(sports);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPut("UpdateSports")]
        public async Task<IActionResult> UpdateSports(Sports sports)
        {
            try
            {
                var res = await sportsRepository.UpdateSport(sports);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpDelete("DeleteSports")]
        public async Task<IActionResult> DeleteSports(int id )
        {
            try
            {
                var res = await sportsRepository.DeleteSport(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
