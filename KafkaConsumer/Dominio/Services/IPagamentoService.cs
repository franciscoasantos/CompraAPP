using KafkaConsumer.Dominio.Model;

namespace KafkaConsumer.Dominio.Services
{
    public interface IPagamentoService
    {
        void ProcessarPagamentoCartao(long idPedido, Cartao cartao);
    }
}
