
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Abstractions.Facades;
using Enums;

namespace ServerioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorioController : ControllerBase
    {
        private readonly ILogger<FactorioController> _logger;
        private readonly IServiceManager _serviceManager;

        public FactorioController(ILogger<FactorioController> logger,
                                  IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet("start")]
        public async Task<IActionResult> Start()
        {
            try
            {
                await _serviceManager.FactorioService.StartSession();
                _logger.LogInformation("Successfully started Factory.exe.");
                return Ok("Successfully started Factory.exe.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting Factory.exe.");
                return StatusCode(500, $"Error starting Factory.exe: {ex.Message}");
            }
        }

        [HttpGet("stop")]
        public async Task<IActionResult> Stop()
        {

            try
            {
                await _serviceManager.FactorioService.StopSession();
                _logger.LogInformation("Successfully stopped Factory.exe.");
                return Ok("Successfully stopped Factory.exe.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error stopping Factory.exe.");
                return StatusCode(500, $"Error stopping Factory.exe: {ex.Message}");
            }

        }

        //[HttpGet("base-info")]
        //public IActionResult Info()
        //{
        //    try
        //    {
        //        return Ok(_processService.GetServerBaseInfo());
        //    }
        //    catch
        //    {
        //        return StatusCode(500, $"Error getting server info");
        //    }
        //}

        //[HttpGet("full-info")]
        //public IActionResult FullInfo()
        //{
        //    try
        //    {
        //        return Ok(_processService.GetGameInfo());
        //    }
        //    catch
        //    {
        //        return StatusCode(500, $"Error getting game info");
        //    }
        //}


    }
}
