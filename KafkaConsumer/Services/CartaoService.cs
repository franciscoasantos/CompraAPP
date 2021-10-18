using System;
using System.Threading.Tasks;
using KafkaConsumer.Dominio.Exceptions;
using KafkaConsumer.Dominio.Model;
using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Dominio.Services;

namespace KafkaConsumer.Services
{
    class CartaoService : ICartaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        public CartaoService(ICartaoRepository cartaoRepository)
        {
            _cartaoRepository = cartaoRepository;
        }
        public async Task<bool> IncluirCartao(Cartao cartao)
        {
            try
            {
                if (await _cartaoRepository.ValidarCartaoExistente(cartao) == 0)
                    await _cartaoRepository.IncluirCartao(cartao);

                return true;
            }
            catch (Exception ex)
            {
                throw new PagamentoException(ex.Message);
            }
        }
    }
}
