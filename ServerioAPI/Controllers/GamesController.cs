using Microsoft.AspNetCore.Mvc;
using ServerioAPI.Interfaces;

namespace ServerioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService) 
        {
            _gamesService = gamesService;
        }

        [HttpGet("plainlist")]
        public IActionResult GetPlainList()
        {
            try
            {
                return Ok(_gamesService.GetGamesList());

            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("list")]
        public IActionResult GetList()
        {
            try
            {
                return Ok(_gamesService.GetGamesListWithState());

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
