
using DataAccessLayer;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerioAPI.Interfaces;

namespace ServerioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorioController : ControllerBase
    {
        private readonly ILogger<FactorioController> _logger;
        private readonly IProcessService _processService;

        public FactorioController(IProcessService processService,
                                  ILogger<FactorioController> logger)
        {
            _logger = logger;
            _processService = processService;
        }

        [HttpGet("start")]
        public IActionResult Start()
        {
            try
            {
                _processService.Start();
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
        public IActionResult Stop()
        {

            try
            {
                _processService.Stop();
                _logger.LogInformation("Successfully stopped Factory.exe.");
                return Ok("Successfully stopped Factory.exe.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error stopping Factory.exe.");
                return StatusCode(500, $"Error stopping Factory.exe: {ex.Message}");
            }

        }

        [HttpGet("base-info")]
        public IActionResult Info()
        {
            try
            {
                return Ok(_processService.GetServerBaseInfo());
            }
            catch
            {
                return StatusCode(500, $"Error getting server info");
            }
        }

        [HttpGet("full-info")]
        public IActionResult FullInfo()
        {
            try
            {
                return Ok(_processService.GetGameInfo());
            }
            catch
            {
                return StatusCode(500, $"Error getting game info");
            }
        }

    }
}
