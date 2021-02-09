using FluentAssertions;
using LocacaoCarro.Dominio.Entidades;
using Xunit;

namespace LocacaoCarro.Testes.Dominio.ObjetosValor
{
    public class NomeTest
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "   ")]
        [InlineData("a", "a")]
        [InlineData("x", "")]
        [InlineData("", "z")]
        public void CriarNome_NomeInvalido_Teste(string primeiroNome, string sobrenome)
        {
            var nome = new Nome(primeiroNome, sobrenome);

            nome.Invalid.Should().BeTrue();
            nome.Notifications.Should().Contain(n => n.Property == nameof(Nome.PrimeiroNome));
            nome.Notifications.Should().Contain(n => n.Property == nameof(Nome.Sobrenome));
        }

        [Theory]
        [InlineData("João", "Silva")]
        [InlineData("Maria", "Couto")]
        [InlineData("carlos", "souza")]
        public void CriarNome_NomeValido_Teste(string primeiroNome, string sobrenome)
        {
            var nome = new Nome(primeiroNome, sobrenome);

            nome.Valid.Should().BeTrue();
        }

        [Theory]
        [InlineData("Maria", "Couto")]
        [InlineData("Carlos", "Souza")]
        public void CriarNome_NomeValido_NomeCompleto_Teste(string primeiroNome, string sobrenome)
        {
            var nome = new Nome(primeiroNome, sobrenome);
            var valorEsperado = $"{primeiroNome} {sobrenome}";

            nome.NomeCompleto.Should().Be(valorEsperado);
            nome.ToString().Should().Be(valorEsperado);
        }
    }
}
