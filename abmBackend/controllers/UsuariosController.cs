using abm.models;
using abm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace abm.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        //agrego el servicio
        private readonly UsuarioServices _usuarioServices;

        public UsuariosController (UsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpPost] //método post
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario usuario ) //recibe un json en el cuerpo de la peticion (Postman)
        {
            try
            {
                var (usuarioCreado, mensaje) = await _usuarioServices.CrearUsuario(usuario);

                if(usuarioCreado != null)
                {
                    return Ok(mensaje);
                }
                else
                {
                    return BadRequest(new { usuario, mensaje });
                }

            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }

        }
    }
}
