using abm.data;
using abm.models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;

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
            var usuarioExiste = (from user in _context.Usuarios
                               where user.dni == nuevoUsuario.dni
                               select user).FirstOrDefault();

            if (usuarioExiste != null) 
            {
                throw new InvalidOperationException("El Usuario ya está registrado");
            }

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return nuevoUsuario;        
        }


    }
}
