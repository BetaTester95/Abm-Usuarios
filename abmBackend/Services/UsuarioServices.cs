using abm.data;
using abm.models;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
            isValidNombreYApellido(nuevoUsuario.nombre, nuevoUsuario.apellido);

            if (!isValidEmail(nuevoUsuario.email))
            {
                throw new ArgumentException("El formato de email ingresado no es valido");
            }

            isValidDni(nuevoUsuario.dni);

            var usuarioExiste = (from user in _context.Usuarios
                                 where user.dni == nuevoUsuario.dni
                                 select user).FirstOrDefault();

            if (usuarioExiste != null)
            {
                throw new InvalidOperationException("El Usuario ya está registrado");
            }

            isValidPassword(nuevoUsuario.password);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return nuevoUsuario;
        }

        private void isValidNombreYApellido(string nombre, string apellido)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Debe ingresar un nombre");
            }
            if (Regex.IsMatch(nombre, @"[0-9]"))
            {
                throw new ArgumentException("El nombre no puede contener números");
            }

            if (string.IsNullOrEmpty(apellido))
            {
                throw new ArgumentException("Debe ingresar un apellido");
            }
            if (Regex.IsMatch(apellido, @"[0-9]"))
            {
                throw new ArgumentException("El apellido no puede contener números");
            }
        }

        private bool isValidEmail(string email)
        {
            try
            {
                //uso de la directiva System.net.Mail
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void isValidDni(int dni)
        {
            string dniString = dni.ToString();

            if (string.IsNullOrEmpty(dniString))
                throw new ArgumentException("El DNI no puede estar vacío");

            // Expresión regular para validar solo números
            if (!Regex.IsMatch(dniString, @"^\d+$"))
            {
                throw new ArgumentException("El DNI debe contener solo números");
            }

            if (!Regex.IsMatch(dniString, @"^\d{8}$"))
            {
                throw new ArgumentException("El DNI debe tener 8 dígitos");

            }
        }

        private void isValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("La contraseña no puede estar vacía");
            }

            //agregamos algunas reestricciones al momento de crear la contraseña
            if (password.Length < 8)
            {
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");
            }
            if (!Regex.IsMatch(password, @"[0-9]")) 
            {
                throw new ArgumentException("La contraseña debe contener al menos un número");
            }
            if (!Regex.IsMatch(password, @"[A-Z]")) 
            {
                throw new ArgumentException("La contraseña debe contener al menos una letra mayúscula");
            }
        }
    }
}
