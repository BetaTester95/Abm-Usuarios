using abm.data;
using abm.models;
using abm.validators;

namespace abm.Services
{

    class UsuarioServices
    {
        private readonly AppDbContext _context;
        private readonly Validators _validators;

        public UsuarioServices(AppDbContext context, Validators validators)
        {
            _context = context;
            _validators = validators;
        }

        //metodo para crear nuevo usuario
        public async Task<Usuario> CrearUsuario(Usuario nuevoUsuario)
        {
            _validators.isValidNombreYApellido(nuevoUsuario.nombre, nuevoUsuario.apellido);

            if (!_validators.isValidEmail(nuevoUsuario.email))
            {
                throw new ArgumentException("El formato de email ingresado no es valido");
            }

            _validators.isValidDni(nuevoUsuario.dni);

            var usuarioExiste = (from user in _context.Usuarios
                                 where user.dni == nuevoUsuario.dni
                                 select user).FirstOrDefault();

            if (usuarioExiste != null)
            {
                throw new InvalidOperationException("El Usuario ya está registrado");
            }

            _validators.isValidPassword(nuevoUsuario.password);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return nuevoUsuario;
        }
    }
}
