using FluentAssertions;
using LocacaoCarro.Aplicacao;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Repositorios;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LocacaoCarro.Testes.Aplicacao
{
    public class UsuarioApplicacaoTest
    {
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;
        private readonly Mock<IOperadorRepositorio> _operadorRepositorio;

        private readonly UsuarioApplicacao _usuarioApplicacao;

        private readonly Cliente _clientePadrao;

        public UsuarioApplicacaoTest()
        {
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _operadorRepositorio = new Mock<IOperadorRepositorio>();

            _usuarioApplicacao = new UsuarioApplicacao(_clienteRepositorio.Object, _operadorRepositorio.Object);

            _clientePadrao = new Cliente(
                new Nome("Letícia", "Gomes"),
                new Cpf("26786093086"),
                new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", string.Empty, "Belo Horizonte", "MG"),
                new DateTime(1991, 1, 1)
            );
        }

        #region ObterClienteAsync

        [Fact]
        public async Task ObterClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.ObterClienteAsync(new Cpf("12345678900"));

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task ObterClienteAsync_ClienteExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.ObterClienteAsync(new Cpf("12345678900"));

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion ObterClienteAsync

        #region SalvarClienteAsync

        [Fact]
        public async Task SalvarClienteAsync_ClienteInvalido_NotificacoesDadosInvalidos()
        {
            var clienteInvalido = new Cliente(null, null, null, DateTime.MinValue, null);

            var resultado = await _usuarioApplicacao.SalvarClienteAsync(clienteInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().HaveSameCount(clienteInvalido.Notifications);
        }

        [Fact]
        public async Task SalvarClienteAsync_ClienteExiste_NotificacaoClienteJaExistente()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.SalvarClienteAsync(_clientePadrao);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task SalvarClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));
            _clienteRepositorio.Setup(x => x.Incluir(It.IsAny<Cliente>())).Verifiable();

            var resultado = await _usuarioApplicacao.SalvarClienteAsync(_clientePadrao);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        #endregion SalvarClienteAsync

        #region AtualizarClienteAsync

        [Fact]
        public async Task AtualizarClienteAsync_ClienteInvalido_NotificacoesDadosInvalidos()
        {
            var clienteInvalido = new Cliente(null, null, null, DateTime.MinValue, null);

            var resultado = await _usuarioApplicacao.AtualizarClienteAsync(clienteInvalido.Cpf, clienteInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().HaveSameCount(clienteInvalido.Notifications);
        }

        [Fact]
        public async Task AtualizarClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.AtualizarClienteAsync(_clientePadrao.Cpf, _clientePadrao);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task AtualizarClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));
            _clienteRepositorio.Setup(x => x.Atualizar(It.IsAny<string>(), It.IsAny<Cliente>())).Verifiable();

            var resultado = await _usuarioApplicacao.AtualizarClienteAsync(_clientePadrao.Cpf, _clientePadrao);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        #endregion AtualizarClienteAsync

        #region RemoverClienteAsync

        [Fact]
        public async Task RemoverClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.RemoverClienteAsync(_clientePadrao.Cpf);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task RemoverClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));
            _clienteRepositorio.Setup(x => x.Excluir(It.IsAny<string>())).Verifiable();

            var resultado = await _usuarioApplicacao.RemoverClienteAsync(_clientePadrao.Cpf);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        # endregion RemoverClienteAsync
    }
}
