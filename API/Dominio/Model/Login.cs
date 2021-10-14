namespace API.Dominio.Model
{
    public class Login
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
    public class LoginResponse
    {
        public bool Logado { get; set; }
    }

}
