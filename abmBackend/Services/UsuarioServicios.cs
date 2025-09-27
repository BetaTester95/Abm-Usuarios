using abm.data;
using Microsoft.IdentityModel.Tokens;

namespace abm.Services
{

    public class UsuarioServicios
    {
        private readonly AppDbContext _context;

        public UsuarioServicios(AppDbContext context)
        {
            _context = context;
        }


    }
}
