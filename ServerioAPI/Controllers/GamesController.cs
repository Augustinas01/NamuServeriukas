using Microsoft.AspNetCore.Mvc;

namespace ServerioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        public GamesController() 
        {

        }

        //[HttpGet("plainlist")]
        //public IActionResult GetPlainList()
        //{
        //    try
        //    {
        //        return Ok(_gamesService.GetGamesList());

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpGet("list")]
        //public IActionResult GetList()
        //{
        //    try
        //    {
        //        return Ok(_gamesService.GetGamesListWithState());

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
