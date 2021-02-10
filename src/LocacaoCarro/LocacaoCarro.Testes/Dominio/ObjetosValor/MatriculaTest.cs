using FluentAssertions;
using LocacaoCarro.Dominio.Entidades;
using Xunit;

namespace LocacaoCarro.Testes.Dominio.ObjetosValor
{
    public class MatriculaTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("asdfghjkloi")]
        [InlineData("1234567890a")]
        public void CriarMatricula_MatriculaInvalido_Teste(string numero)
        {
            var matricula = new Matricula(numero);

            matricula.Invalid.Should().BeTrue();
            matricula.Notifications.Should().Contain(n => n.Property == nameof(matricula.Numero));
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("0987654")]
        public void CriarMatricula_MatriculaValido_Teste(string numero)
        {
            var matricula = new Matricula(numero);

            matricula.Valid.Should().BeTrue();
        }

        [Fact]
        public void CriarMatricula_MatriculaValido_GetCopy_Teste()
        {
            var matricula = new Matricula("12345678901");
            var matriculaClone = (Matricula)matricula.GetCopy();

            matriculaClone.Numero.Should().Be(matricula.Numero);
        }

        [Fact]
        public void Matricula_ToString_Teste()
        {
            var matricula = new Matricula("0987654");

            matricula.ToString().Should().Be(matricula.Numero);
        }
    }
}