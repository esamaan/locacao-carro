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

        public async Task<Resultado<Cliente>> ConsultarClienteAsync(Cpf cpf)
        {
            var cliente = await _clienteRepositorio.Consultar(cpf.Numero);

            if (cliente == null)
                return Resultado<Cliente>.Erro(nameof(Cliente), "Cliente não encontrado");

            return Resultado<Cliente>.Ok(cliente);
        }

        public async Task<Resultado> CriarClienteAsync(Cliente cliente)
        {
            if (!cliente.Valid)
                return Resultado.Erro(cliente.Notifications);

            var clienteExistente = await _clienteRepositorio.Consultar(cliente.Cpf.Numero);

            if (clienteExistente != null)
                return Resultado.Erro(nameof(Cliente), "Cliente já cadastrado");

            await _clienteRepositorio.Criar(cliente);

            return Resultado.Ok();
        }

        public async Task<Resultado> AtualizarClienteAsync(Cpf cpf, Cliente cliente)
        {
            if (!cliente.Valid)
                return Resultado.Erro(cliente.Notifications);

            var clienteExistente = await _clienteRepositorio.Consultar(cliente.Cpf.Numero);

            if (clienteExistente == null)
                return Resultado.Erro(nameof(Cliente), "Cliente não encontrado");

            await _clienteRepositorio.Atualizar(cpf.Numero, cliente);

            return Resultado.Ok();
        }

        public async Task<Resultado> RemoverClienteAsync(Cpf cpf)
        {
            var clienteExistente = await _clienteRepositorio.Consultar(cpf.Numero);

            if (clienteExistente == null)
                return Resultado.Erro(nameof(Cliente), "Cliente não encontrado");

            await _clienteRepositorio.Remover(cpf.Numero);

            return Resultado.Ok();
        }

        
    }
}
