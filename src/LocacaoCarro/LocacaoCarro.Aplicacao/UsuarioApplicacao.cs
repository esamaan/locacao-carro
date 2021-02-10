using Flunt.Notifications;
using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Resultados;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.Repositorios;
using System.Threading.Tasks;

namespace LocacaoCarro.Aplicacao
{
    public class UsuarioApplicacao : IUsuarioApplicacao
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;

        public UsuarioApplicacao(IClienteRepositorio clienteRepositorio, IOperadorRepositorio operadorRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _operadorRepositorio = operadorRepositorio;
        }

        public async Task<Resultado<Cliente>> ObterClienteAsync(string cpf)
        {
            var cliente = await _clienteRepositorio.Obter(cpf);

            if (cliente == null)
                return Resultado<Cliente>.Erro(nameof(Cliente), "Cliente não encontrado");

            return Resultado<Cliente>.Ok(cliente);
        }

        public Task<Resultado> SalvarClienteAsync(Cliente cliente)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado> AtualizarClienteAsync(string cpf, Cliente cliente)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resultado> RemoverClienteAsync(string cpf)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
