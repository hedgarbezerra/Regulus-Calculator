using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Regulus.API.Controllers.v1
{
    [ApiVersion("1.0", Deprecated = true)]
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

        [HttpGet, MapToApiVersion("1.1")]
        [Route("helloworld")]
        public IActionResult Get1()
        {
            return Ok("Hello Stage");
        }

        [HttpGet]
        public IActionResult Get()
        {
            if(_env is null)
                return NoContent();

            using(var writer = new StringWriter())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, _env);
                return Ok(writer.ToString());
            }
        }
    }
}
