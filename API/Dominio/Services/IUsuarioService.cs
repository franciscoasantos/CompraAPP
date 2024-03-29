﻿using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface IUsuarioService
    {
        Task<CadastroResponse> Cadastrar(Cadastro cadastro);
    }
}
