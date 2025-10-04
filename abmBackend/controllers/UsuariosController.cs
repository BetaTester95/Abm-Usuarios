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
                var usuarioCreado = await _usuarioServices.CrearUsuario(usuario);
                return Ok(usuarioCreado);

            }
            catch(SqlException e)
            {
                return StatusCode(503, new { mensaje = "Error de conexión a la base de datos" });
            }
            catch (Exception e) 
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor" }); //error que me impide procesar la solicitud

            }

        }
    }
}
