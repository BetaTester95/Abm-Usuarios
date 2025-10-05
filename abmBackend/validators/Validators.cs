using System.Net.Mail;
using System.Text.RegularExpressions;

namespace abm.validators
{
    public class Validators
    {
        public string? isValidNombreYApellido(string nombre, string apellido)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return "Debe ingresar un nombre";
            }
            if (Regex.IsMatch(nombre, @"[0-9]"))
            {
                return "El nombre no puede contener números";
            }

            if (string.IsNullOrEmpty(apellido))
            {
                return "Debe ingresar un apellido";
            }
            if (Regex.IsMatch(apellido, @"[0-9]"))
            {
                return "El apellido no puede contener números";
            }

            return null; // todo valido, no devuelve nada
        }

        public string? isValidEmail(string email)
        {
            var addr = new MailAddress(email);
            if (addr.Address != email)
            {
                return "Atencion! El formato de email ingresado no es válido";
            }
            return null;
        }

        public string? isValidDni(int dni)
        {
            string dniString = dni.ToString();

            if (string.IsNullOrEmpty(dniString))
                return "El DNI no puede estar vacío";

            // Expresión regular para validar solo números
            if (!Regex.IsMatch(dniString, @"^\d+$"))
            {
                return "El DNI debe contener solo números";
            }

            if (!Regex.IsMatch(dniString, @"^\d{8}$"))
            {
                return "El DNI debe tener 8 dígitos";

            }

            return null;
        }
        public string? isValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "La contraseña no puede estar vacía";
            }

            //agregamos algunas reestricciones al momento de crear la contraseña
            if (password.Length < 8)
            {
                return "La contraseña debe tener al menos 8 caracteres";
            }
            if (!Regex.IsMatch(password, @"[0-9]")) 
            {
                return "La contraseña debe contener al menos un número";
            }
            if (!Regex.IsMatch(password, @"[A-Z]")) 
            {
                return "La contraseña debe contener al menos una letra mayúscula";
            }

            return null;
        }
    }
}
