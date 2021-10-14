using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;

namespace API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CadastroResponse> Cadastrar(Cadastro cadastro)
        {
            return await _usuarioRepository.Cadastrar(cadastro);
        }

        public async Task<LoginResponse> Login(Login login)
        {
            return await _usuarioRepository.Login(login);
        }
    }
}
