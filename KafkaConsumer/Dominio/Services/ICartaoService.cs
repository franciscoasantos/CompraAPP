using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;

namespace KafkaConsumer.Dominio.Services
{
    public interface ICartaoService
    {
        public Task<bool> IncluirCartao(Cartao cartao);
    }
}
