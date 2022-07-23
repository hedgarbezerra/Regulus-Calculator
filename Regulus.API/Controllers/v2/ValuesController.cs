using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Regulus.API.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet, MapToApiVersion("1.1")]
        [Route("helloworld1")]
        public IActionResult Get1()
        {
            return Ok("Hello Stage");
        }
        [HttpGet]
        [Route("helloworld")]
        public IActionResult Get()
        {
            return Ok("Hello Stage");
        }
    }
}
