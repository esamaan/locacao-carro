using FluentAssertions;
using LocacaoCarro.Dominio.ObjetosValor;
using Xunit;

namespace LocacaoCarro.Testes.Dominio.ObjetosValor
{
    public class EnderecoTest
    {
        [Fact]
        public void CriarEndereco_EnderecoInvalido_Teste()
        {
            var endereco = new Endereco(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            endereco.Invalid.Should().BeTrue();
            endereco.Notifications.Should().Contain(n => n.Property == nameof(Endereco.Cep));
            endereco.Notifications.Should().Contain(n => n.Property == nameof(Endereco.Logradouro));
            endereco.Notifications.Should().Contain(n => n.Property == nameof(Endereco.Cidade));
            endereco.Notifications.Should().Contain(n => n.Property == nameof(Endereco.Estado));
        }

        [Fact]
        public void CriarEndereco_EnderecoValido_Teste()
        {
            var endereco = new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", string.Empty, "Belo Horizonte", "MG");

            endereco.Valid.Should().BeTrue();
        }

        [Fact]
        public void Endereco_EnderecoComposto_Teste()
        {
            var endereco = new Endereco("31150-900", "Av. Bernardo de Vasconcelos", "377", "A", "Belo Horizonte", "MG");

            endereco.ToString().Should().Be($"Av. Bernardo de Vasconcelos, 377/A, Belo Horizonte - MG. CEP: 31150-900");
        }
    }
}
