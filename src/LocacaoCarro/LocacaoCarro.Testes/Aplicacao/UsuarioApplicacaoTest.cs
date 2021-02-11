using FluentAssertions;
using LocacaoCarro.Aplicacao;
using LocacaoCarro.Aplicacao.Modelos;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Testes.Fixture;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LocacaoCarro.Testes.Aplicacao
{
    [Collection("Mapper")]
    public class UsuarioApplicacaoTest
    {
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;
        private readonly Mock<IOperadorRepositorio> _operadorRepositorio;
        private readonly MapperFixture _mapperFixture;

        private readonly UsuarioApplicacao _usuarioApplicacao;

        private readonly Cliente _clientePadrao;
        private readonly ClienteModel _clienteModelPadrao;

        public UsuarioApplicacaoTest(MapperFixture mapperFixture)
        {
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _operadorRepositorio = new Mock<IOperadorRepositorio>();
            _mapperFixture = mapperFixture;

            _usuarioApplicacao = new UsuarioApplicacao(_clienteRepositorio.Object, _operadorRepositorio.Object, _mapperFixture.Mapper);

            _clientePadrao = new Cliente(
                new Nome("Letícia", "Gomes"),
                new Cpf("26786093086"),
                new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", string.Empty, "Belo Horizonte", "MG"),
                new DateTime(1991, 1, 1)
            );

            _clienteModelPadrao = new ClienteModel
            {
                Aniversario = new DateTime(1991, 1, 1),
                Cpf = "26786093086",
                Nome = "Letícia",
                Sobrenome = "Gome",
                Endereco = new EnderecoModel
                {
                    Cep = "31150-900",
                    Logradouro = "Av. Bernardo de Vasconcelos",
                    Numero = "377",
                    Complemento = string.Empty,
                    Cidade = "Belo Horizonte",
                    Estado = "MG"
                }
            };
        }

        #region ObterClienteAsync

        [Fact]
        public async Task ObterClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.ConsultarClienteAsync("12345678900");

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task ObterClienteAsync_ClienteExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.ConsultarClienteAsync("12345678900");

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion ObterClienteAsync

        #region SalvarClienteAsync

        [Fact]
        public async Task SalvarClienteAsync_ClienteInvalido_NotificacoesDadosInvalidos()
        {
            var clienteInvalido = new ClienteModel();

            var resultado = await _usuarioApplicacao.CriarClienteAsync(clienteInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().NotBeEmpty();
        }

        [Fact]
        public async Task SalvarClienteAsync_ClienteExiste_NotificacaoClienteJaExistente()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.CriarClienteAsync(_clienteModelPadrao);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task SalvarClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));
            _clienteRepositorio.Setup(x => x.Criar(It.IsAny<Cliente>())).Verifiable();

            var resultado = await _usuarioApplicacao.CriarClienteAsync(_clienteModelPadrao);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        #endregion SalvarClienteAsync

        #region AtualizarClienteAsync

        [Fact]
        public async Task AtualizarClienteAsync_ClienteInvalido_NotificacoesDadosInvalidos()
        {
            var clienteInvalido = new ClienteModel();

            var resultado = await _usuarioApplicacao.AtualizarClienteAsync(clienteInvalido.Cpf, clienteInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().NotBeEmpty();
        }

        [Fact]
        public async Task AtualizarClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.AtualizarClienteAsync(_clienteModelPadrao.Cpf, _clienteModelPadrao);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task AtualizarClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));
            _clienteRepositorio.Setup(x => x.Atualizar(It.IsAny<string>(), It.IsAny<Cliente>())).Verifiable();

            var resultado = await _usuarioApplicacao.AtualizarClienteAsync(_clienteModelPadrao.Cpf, _clienteModelPadrao);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        #endregion AtualizarClienteAsync

        #region RemoverClienteAsync

        [Fact]
        public async Task RemoverClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.RemoverClienteAsync(_clienteModelPadrao.Cpf);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task RemoverClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));
            _clienteRepositorio.Setup(x => x.Remover(It.IsAny<string>())).Verifiable();

            var resultado = await _usuarioApplicacao.RemoverClienteAsync(_clienteModelPadrao.Cpf);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        # endregion RemoverClienteAsync
    }
}
