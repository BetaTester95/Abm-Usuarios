using abm.models;
using abm.validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using abm.Services;

namespace abm.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly Validators _validators;
        private readonly UsuarioServices _usuarioServices;

        private PersonasController(Validators validators, UsuarioServices usuarioServices)
        {
            _validators = validators;
            _usuarioServices = usuarioServices;
        }

    }
}
