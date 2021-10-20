using System;
using KafkaConsumer.Dominio.Exceptions;
using KafkaConsumer.Dominio.Model;
using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Dominio.Services;

namespace KafkaConsumer.Services
{
    class PagamentoService : IPagamentoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PagamentoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async void ProcessarPagamentoCartao(long idPedido, Cartao cartao)
        {
            try
            {
                //Status Pagamento Aprovado
                var statusPedido = "A";
                await _pedidoRepository.AtualizarStatusPedido(idPedido, statusPedido);
            }
            catch (Exception ex)
            {
                throw new PagamentoException(ex.Message, ex.InnerException);
            }
        }
    }
}
