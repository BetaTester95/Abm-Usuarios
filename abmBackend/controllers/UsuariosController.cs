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

        public UsuariosController(UsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }


        [HttpPost("crearUsuario")] //método post
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario usuario ) //recibe un json en el cuerpo de la peticion (Postman)
        {
            try
            {
                var (usuarioCreado, mensaje) = await _usuarioServices.CrearUsuario(usuario);

                    return Ok(mensaje);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("editarUsuario/{id}")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] Usuario usuario)
        {
            try
            {
                var (usuarioEditado, mensaje) = await _usuarioServices.EditarUsuario(id, usuario);
                return Ok(mensaje);
            }
            catch (Exception e) 
            { 
                throw new Exception(e.Message);           
            }
        }


        [HttpGet("obtenerUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var lista = await _usuarioServices.ObtenerUsuarios();
            return Ok(lista);
        }

        [HttpDelete("eliminarUsuario/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var resultado = await _usuarioServices.EliminarUsuario(id);

            return Ok(new { Message = resultado });
        }
    }
}
