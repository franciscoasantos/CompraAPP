using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface ITokenService
    {
        string GerarToken(Login login);
    }
}
