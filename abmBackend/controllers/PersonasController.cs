using Microsoft.AspNetCore.Mvc;

namespace abm.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {

        public IActionResult agregarPersonas()
        {

            return Ok();
        }
    }
}
