namespace API.Dominio.Services
{
    interface ICriptografiaService
    {
        public string Criptografar(string senha);
        public string Descriptografar(string senha);
    }
}
