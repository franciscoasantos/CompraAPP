using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Services;
using Dapper;

namespace API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbSession _sessao;

        public UsuarioRepository(DbSession sessao)
        {
            _sessao = sessao;
        }
        
        public Task<CadastroResponse> Cadastrar(Cadastro cadastro)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> Login(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
