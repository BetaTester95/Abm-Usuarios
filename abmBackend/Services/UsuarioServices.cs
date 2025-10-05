using abm.data;
using abm.models;
using abm.validators;
using Microsoft.EntityFrameworkCore;

namespace abm.Services
{
    public class UsuarioServices
    {
        //variables de solo lectura, no se pueden modificar
        private readonly AppDbContext _context;
        private readonly Validators _validators;

        public UsuarioServices(AppDbContext context, Validators validators)
        {
            _context = context;
            _validators = validators;
        }

        //metodo para crear nuevo usuario
        public async Task<(Usuario, string mensaje)> CrearUsuario(Usuario nuevoUsuario)
        {
            string mensajeError;

            mensajeError = _validators.isValidNombreYApellido(nuevoUsuario.nombre, nuevoUsuario.apellido);
            if (mensajeError != null)
            {
                return (null, mensajeError);
            }

            mensajeError = _validators.isValidEmail(nuevoUsuario.email);
            if (mensajeError != null)
            {
                return (null, mensajeError);
            }


            mensajeError = _validators.isValidDni(nuevoUsuario.dni);
            if (mensajeError != null)
            {
                return (null, mensajeError);
            }

            var usuarioExiste = await (from user in _context.Usuarios
                                       where user.dni == nuevoUsuario.dni
                                       select user).FirstOrDefaultAsync();

            if (usuarioExiste != null)
            {
                return (null, "Error! El Usuario ya está registrado");
            }

            mensajeError = _validators.isValidPassword(nuevoUsuario.password);
            if (mensajeError != null)
            {
                return (null, mensajeError);
            }


            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return (nuevoUsuario, "Usuario creado correctamente");
        }
    }
}
