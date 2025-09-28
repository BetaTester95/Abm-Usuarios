using abm.data;
using abm.models;
using abm.validators;

namespace abm.Services
{

    public class UsuarioServices
    {
        private readonly AppDbContext _context;

        public UsuarioServices(AppDbContext context)
        {
            _context = context;
        }

        //metodo para crear nuevo usuario
        public async Task<Usuario> CrearUsuario(Usuario nuevoUsuario)
        {
            Validators validator = new Validators();

            validator.isValidNombreYApellido(nuevoUsuario.nombre, nuevoUsuario.apellido);

            if (!validator.isValidEmail(nuevoUsuario.email))
            {
                throw new ArgumentException("El formato de email ingresado no es valido");
            }

            validator.isValidDni(nuevoUsuario.dni);

            var usuarioExiste = (from user in _context.Usuarios
                                 where user.dni == nuevoUsuario.dni
                                 select user).FirstOrDefault();

            if (usuarioExiste != null)
            {
                throw new InvalidOperationException("El Usuario ya está registrado");
            }

            validator.isValidPassword(nuevoUsuario.password);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return nuevoUsuario;
        }

    }
}
