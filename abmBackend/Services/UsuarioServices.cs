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
        public string Message { get; private set; } = string.Empty;

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

            //encriptar la contraseña ingresada por el usuario
            nuevoUsuario.password = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.password);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();


            return (nuevoUsuario, "Usuario creado correctamente");
        }


        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            Message = usuarios.Count == 0 ? Message = "No hay usuarios registrados" : Message = "Usuarios encontrados";

            return usuarios;
        }

        public async Task<string> EliminarUsuario(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return "Error! Usuario no encontrado";
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return "Usuario eliminado correctamente";
        }


    }
}
