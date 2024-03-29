﻿using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface IPedidoService
    {
        public Task<PedidoResponse> CriarPedido(string user, Pedido pedido);
    }
}
