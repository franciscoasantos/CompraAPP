namespace API.Dominio.Services
{
    public interface ICriptografiaService
    {
        public string Criptografar(string senha);
        public string Descriptografar(string senha);
    }
}
