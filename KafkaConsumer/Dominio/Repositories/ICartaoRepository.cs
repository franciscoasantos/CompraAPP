using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;

namespace KafkaConsumer.Dominio.Repositories
{
    public interface ICartaoRepository
    {
        public Task<int> IncluirCartao(Cartao cartao);
        public Task<int> ValidarCartaoExistente(Cartao cartao);
    }
}
