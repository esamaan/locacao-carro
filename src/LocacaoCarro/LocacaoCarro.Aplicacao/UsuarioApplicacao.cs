using AutoMapper;
using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Modelos;
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
        private readonly IMapper _mapper;

        public UsuarioApplicacao(IClienteRepositorio clienteRepositorio, IOperadorRepositorio operadorRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _operadorRepositorio = operadorRepositorio;
            _mapper = mapper;
        }

        public async Task<Resultado<ClienteModel>> ConsultarClienteAsync(string cpf)
        {
            var cpfObj = new Cpf(cpf);

            var cliente = await _clienteRepositorio.Consultar(cpfObj.Numero);

            if (cliente == null)
                return Resultado<ClienteModel>.Erro(nameof(Cliente), "Cliente não encontrado");

            return Resultado<ClienteModel>.Ok(_mapper.Map< Cliente, ClienteModel>(cliente));
        }

        public async Task<Resultado> CriarClienteAsync(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);

            if (!cliente.Valid)
                return Resultado.Erro(cliente.Notifications);

            var clienteExistente = await _clienteRepositorio.Consultar(cliente.Cpf.Numero);

            if (clienteExistente != null)
                return Resultado.Erro(nameof(Cliente), "Cliente já cadastrado");

            await _clienteRepositorio.Criar(cliente);

            return Resultado.Ok();
        }

        public async Task<Resultado> AtualizarClienteAsync(string cpf, ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            var cpfObj = new Cpf(cpf);

            if (!cliente.Valid)
                return Resultado.Erro(cliente.Notifications);

            var clienteExistente = await _clienteRepositorio.Consultar(cliente.Cpf.Numero);

            if (clienteExistente == null)
                return Resultado.Erro(nameof(Cliente), "Cliente não encontrado");

            await _clienteRepositorio.Atualizar(cpfObj.Numero, cliente);

            return Resultado.Ok();
        }

        public async Task<Resultado> RemoverClienteAsync(string cpf)
        {
            var cpfObj = new Cpf(cpf);

            var clienteExistente = await _clienteRepositorio.Consultar(cpfObj.Numero);

            if (clienteExistente == null)
                return Resultado.Erro(nameof(Cliente), "Cliente não encontrado");

            await _clienteRepositorio.Remover(cpfObj.Numero);

            return Resultado.Ok();
        }

        
    }
}
