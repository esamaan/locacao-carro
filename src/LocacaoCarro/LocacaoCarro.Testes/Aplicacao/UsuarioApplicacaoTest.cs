using FluentAssertions;
using LocacaoCarro.Aplicacao;
using LocacaoCarro.Aplicacao.Modelos.Usuarios;
using LocacaoCarro.Dominio.Entidades.Usuarios;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Testes.Fixture;
using Microsoft.Extensions.Configuration;
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
        private readonly Mock<IConfiguration> _configuration;

        private readonly UsuarioApplicacao _usuarioApplicacao;

        private readonly Cliente _clientePadrao;
        private readonly ClienteModel _clienteModelPadrao;

        private readonly Operador _operadorPadrao;

        public UsuarioApplicacaoTest(MapperFixture mapperFixture)
        {
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _operadorRepositorio = new Mock<IOperadorRepositorio>();
            _mapperFixture = mapperFixture;
            _configuration = new Mock<IConfiguration>();

            _usuarioApplicacao = new UsuarioApplicacao(_clienteRepositorio.Object, _operadorRepositorio.Object, _mapperFixture.Mapper, _configuration.Object);

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
                },
                Senha = "12345"
            };

            _operadorPadrao = new Operador(
                new Matricula("12345"),
                new Nome("Márcia", "Prado")
            );

            _configuration.Setup(x => x["Autenticacao:SegredoTokenJWT"]).Returns("21B81ABC927F6C9A13B13B4D349CBCB77CB839C5252BEF3E13BA2773B92");
        }

        #region ConsultarClienteAsync

        [Fact]
        public async Task ConsultarClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.ConsultarClienteAsync("12345678900");

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task ConsultarClienteAsync_ClienteExiste_Ok()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.ConsultarClienteAsync("12345678900");

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion ConsultarClienteAsync

        #region CriarClienteAsync

        [Fact]
        public async Task CriarClienteAsync_ClienteInvalido_NotificacoesDadosInvalidos()
        {
            var clienteInvalido = new ClienteModel();

            var resultado = await _usuarioApplicacao.CriarClienteAsync(clienteInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CriarClienteAsync_SenhaNaoInformada_NotificacaoSenha()
        {
            _clienteModelPadrao.Senha = string.Empty;

            var resultado = await _usuarioApplicacao.CriarClienteAsync(_clienteModelPadrao);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task CriarClienteAsync_ClienteExiste_NotificacaoClienteJaExistente()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.CriarClienteAsync(_clienteModelPadrao);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task CriarClienteAsync_Ok()
        {
            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));
            _clienteRepositorio.Setup(x => x.Criar(It.IsAny<Cliente>())).Verifiable();

            var resultado = await _usuarioApplicacao.CriarClienteAsync(_clienteModelPadrao);

            resultado.Sucesso.Should().BeTrue();
            _clienteRepositorio.Verify();
        }

        #endregion CriarClienteAsync

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

        #endregion RemoverClienteAsync

        #region ConsultarOperadorAsync

        [Fact]
        public async Task ConsultarOperadorAsync_OperadorNaoExiste_NotificacaoOperadorNaoEncontrado()
        {
            _operadorRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult<Operador>(null));

            var resultado = await _usuarioApplicacao.ConsultarOperadorAsync("12345");

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Operador));
        }

        [Fact]
        public async Task ConsultarOperadorAsync_ClienteExiste_Ok()
        {
            _operadorRepositorio.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(_operadorPadrao));

            var resultado = await _usuarioApplicacao.ConsultarOperadorAsync("12345");

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion ConsultarOperadorAsync

        #region AutenticarClienteAsync

        [Fact]
        public async Task AutenticarClienteAsync_DadosIncorretos_NotificacaoUsuarioSenhaErrado()
        {
            var autenticacaoInputModel = new AutenticacaoInputModel
            {
                Login = "login",
                Senha = "senha"
            };

            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.AutenticarClienteAsync(autenticacaoInputModel);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task AutenticarClienteAsync_DadosCorretos_Ok()
        {
            var autenticacaoInputModel = new AutenticacaoInputModel
            {
                Login = "login",
                Senha = "senha"
            };

            _clienteRepositorio.Setup(x => x.Consultar(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<Cliente>(_clientePadrao));

            var resultado = await _usuarioApplicacao.AutenticarClienteAsync(autenticacaoInputModel);

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion AutenticarClienteAsync

        #region AutenticarOperadorAsync

        [Fact]
        public async Task AutenticarOperadorAsync_DadosIncorretos_NotificacaoUsuarioSenhaErrado()
        {
            var autenticacaoInputModel = new AutenticacaoInputModel
            {
                Login = "login",
                Senha = "senha"
            };

            _operadorRepositorio.Setup(x => x.Consultar(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<Operador>(null));

            var resultado = await _usuarioApplicacao.AutenticarOperadorAsync(autenticacaoInputModel);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Operador));
        }

        [Fact]
        public async Task AutenticarOperadorAsync_DadosCorretos_Ok()
        {
            var autenticacaoInputModel = new AutenticacaoInputModel
            {
                Login = "login",
                Senha = "senha"
            };

            _operadorRepositorio.Setup(x => x.Consultar(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<Operador>(_operadorPadrao));

            var resultado = await _usuarioApplicacao.AutenticarOperadorAsync(autenticacaoInputModel);

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion AutenticarOperadorAsync
    }
}
