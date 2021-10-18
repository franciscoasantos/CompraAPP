namespace KafkaConsumer.Dominio.Model
{
    public class Cartao
    {
        public long IdUsuario { get; set; }
        public string Numero { get; set; }
        public string Vencimento { get; set; }
        public string CodigoSeguranca { get; set; }
    }
}
