
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;


namespace ServerioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceManager _serviceManager;

        public ServiceController(ILogger<ServiceController> logger,
                                  IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet("start/{id}")]
        public async Task<IActionResult> Start(int id)
        {
            try
            {
                await _serviceManager.ExternalServicesManager.Start(id);
                _logger.LogInformation($"Successfully started {id} service");
                return Ok($"Successfully started {id} service");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error starting {id} service");
                return StatusCode(500, $"Error starting {id} service: {ex.Message}");
            }
        }

        [HttpGet("stop/{id}")]
        public async Task<IActionResult> Stop(int id)
        {

            try
            {
                await _serviceManager.ExternalServicesManager.Stop(id);
                _logger.LogInformation($"Successfully stopped {id} service");
                return Ok($"Successfully stopped {id} service");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error stopping {id} service");
                return StatusCode(500, $"Error stopping {id} service: {ex.Message}");
            }

        }

        [HttpGet("info/{id}")]
        public IActionResult Info(int id)
        {
            try
            {
                return Ok(_serviceManager.ExternalServicesManager.GetServiceInfo(id));
            }
            catch
            {
                return StatusCode(500, $"Error getting server info");
            }
        }

        [HttpGet("list")]
        public IActionResult GetList()
        {
            try
            {
                return Ok(
                    _serviceManager
                    .ExternalServicesLibrary
                    .GetAvailableServices());

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
