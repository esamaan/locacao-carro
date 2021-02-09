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
            var Matricula = new Matricula(numero);

            Matricula.Invalid.Should().BeTrue();
            Matricula.Notifications.Should().Contain(n => n.Property == nameof(Matricula.Numero));
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("0987654")]
        public void CriarMatricula_MatriculaValido_Teste(string numero)
        {
            var Matricula = new Matricula(numero);

            Matricula.Valid.Should().BeTrue();
        }

        [Fact]
        public void CriarMatricula_MatriculaValido_GetCopy_Teste()
        {
            var Matricula = new Matricula("12345678901");
            var MatriculaClone = (Matricula)Matricula.GetCopy();

            MatriculaClone.Numero.Should().Be(Matricula.Numero);
        }
    }
}