namespace API.Dominio.Model
{
    public class Cadastro
    {
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Sexo { get; set; }
        public Endereco Endereco { get; set; }
    }
    public class CadastroResponse
    {
        public long Id { get; set; }
        public string Detalhe { get; set; }
    }
}
