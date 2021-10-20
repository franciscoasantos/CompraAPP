namespace API.Dominio.Model
{
    public class Pedido
    {
        public long IdAplicativo { get; set; }
        public Cartao Cartao { get; set; }
        public bool SalvarCartao { get; set; }
    }
    public class PedidoMensagem
    {
        public long IdUsuario { get; set; }
        public long IdAplicativo { get; set; }
        public Cartao Cartao { get; set; }
        public bool SalvarCartao { get; set; }
    }
    public class PedidoResponse
    {
        public bool Sucesso { get; set; }
        public string Detalhe { get; set; }
    }
}
