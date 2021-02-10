
using FluentAssertions;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.ObjetosValor;
using System;
using Xunit;

namespace LocacaoCarro.Testes.Dominio
{
    public class ClienteTest
    {
        [Fact]
        public void CriarCliente_ClienteInvalido_Teste()
        {
            var cliente = new Cliente(null, null, null, DateTime.MinValue);

            cliente.Invalid.Should().BeTrue();
            cliente.Notifications.Should().Contain(n => n.Property == nameof(Cliente.Cpf));
            cliente.Notifications.Should().Contain(n => n.Property == nameof(Cliente.Nome));
            cliente.Notifications.Should().Contain(n => n.Property == nameof(Cliente.Endereco));
        }

        [Fact]
        public void CriarCliente_ClienteValido_Teste()
        {
            var cliente = new Cliente(
                new Nome("Letícia", "Gomes"),
                new Cpf("12345678900"),
                new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", string.Empty, "Belo Horizonte", "MG"),
                new DateTime(1991, 1, 1)
            );

            cliente.Valid.Should().BeTrue();
        }

        [Fact]
        public void CriarCliente_VarificacaoCampos_Teste()
        {
            var cliente = new Cliente(
                new Nome("Letícia", "Gomes"),
                new Cpf("12345678900"),
                new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", string.Empty, "Belo Horizonte", "MG"),
                new DateTime(1991, 1, 1)
            );

            cliente.Nome.PrimeiroNome.Should().Be("Letícia");
            cliente.Nome.Sobrenome.Should().Be("Gomes");
            cliente.Cpf.Numero.Should().Be("12345678900");
            cliente.Endereco.ToString().Should().Be($"Av. Bernardo de Vasconcelos, 377, Belo Horizonte - MG. CEP: 31150-900");
            cliente.Aniversario.Should().BeSameDateAs(new DateTime(1991, 1, 1));
        }
    }
}
