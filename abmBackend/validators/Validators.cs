using System.Net.Mail;
using System.Text.RegularExpressions;

namespace abm.validators
{
    public static class Validators
    {
        public static void isValidNombreYApellido(string nombre, string apellido)
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

        public static bool isValidEmail(string email)
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

        public static void isValidDni(int dni)
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
        public static void isValidPassword(string password)
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
