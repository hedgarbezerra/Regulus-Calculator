using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Regulus.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ValuesController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_env.EnvironmentName);
        }
    }
}
