using FluentAssertions;
using LocacaoCarro.Dominio.Entidades;
using Xunit;

namespace LocacaoCarro.Testes.Dominio.ObjetosValor
{
    public class CpfTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("123456789123")]
        [InlineData("123456789101231")]
        [InlineData("asdfghjkloi")]
        [InlineData("1234567890a")]
        [InlineData("12345678901")]
        public void CriarCpf_CpfInvalido_Teste(string numero)
        {
            var cpf = new Cpf(numero);

            cpf.Invalid.Should().BeTrue();
            cpf.Notifications.Should().Contain(n => n.Property == nameof(Cpf.Numero));
        }

        [Theory]
        [InlineData("817.235.160-74")]
        [InlineData("26786093086")]
        public void CriarCpf_CpfValido_Teste(string numero)
        {
            var cpf = new Cpf(numero);

            cpf.Valid.Should().BeTrue();
        }

        [Fact]
        public void CriarCpf_CpfValido_GetCopy_Teste()
        {
            var cpf = new Cpf("12345678901");
            var cpfClone = (Cpf)cpf.GetCopy();

            cpfClone.Numero.Should().Be(cpf.Numero);
        }

        [Fact]
        public void Cpf_ToString_Teste()
        {
            var cpf = new Cpf("12345678901");

            cpf.ToString().Should().Be(cpf.Numero);
        }
    }
}