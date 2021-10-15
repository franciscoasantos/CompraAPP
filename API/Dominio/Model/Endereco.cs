namespace API.Dominio.Model
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public long Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public string PontoDeReferencia { get; set; }

        public override string ToString()
        {
            return $"{Logradouro}, {Numero}, {Complemento}, {Bairro} - {Cidade}/{Estado} ({PontoDeReferencia})";
        }
    }
}
