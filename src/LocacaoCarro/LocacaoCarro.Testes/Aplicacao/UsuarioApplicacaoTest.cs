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
                new Cpf("12345678900"),
                new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", string.Empty, "Belo Horizonte", "MG"),
                new DateTime(1991, 1, 1)
            );
        }

        #region ObterClienteAsync

        [Fact]
        public async Task ObterClienteAsync_ClienteNaoExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult<Cliente>(null));

            var resultado = await _usuarioApplicacao.ObterClienteAsync("12345678900");
            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Cliente));
        }

        [Fact]
        public async Task ObterClienteAsync_ClienteExiste_NotificacaoClienteNaoEncontrado()
        {
            _clienteRepositorio.Setup(x => x.Obter(It.IsAny<string>())).Returns(Task.FromResult(_clientePadrao));

            var resultado = await _usuarioApplicacao.ObterClienteAsync("12345678900");
            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }

        #endregion ObterClienteAsync
    }
}
