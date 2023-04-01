
using DataAccessLayer;
using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerioAPI.Interfaces;
using Services.Abstractions;

namespace ServerioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorioController : ControllerBase
    {
        private readonly ILogger<FactorioController> _logger;
        private readonly IProcessService _processService;
        private readonly IServiceManager _serviceManager;

        public FactorioController(IProcessService processService,
                                  ILogger<FactorioController> logger,
                                  IServiceManager serviceManager)
        {
            _logger = logger;
            _processService = processService;
            _serviceManager = serviceManager;
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

        [HttpGet("test-start")]
        public async Task<IActionResult> Test()
        {
            try
            {
                var id = await _serviceManager.FactorioService.StartSession();
                return Ok(id);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("test-stop")]
        public async Task<IActionResult> TestStop()
        {
            try
            {
                await _serviceManager.FactorioService.StopSession(1);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
