using FluentAssertions;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.Entidades.Usuarios;
using Xunit;

namespace LocacaoCarro.Testes.Dominio
{
    public class OperadorTest
    {
        [Fact]
        public void CriarOperador_OperadorInvalido_Teste()
        {
            var operador = new Operador(null, null);

            operador.Invalid.Should().BeTrue();
            operador.Notifications.Should().Contain(n => n.Property == nameof(Operador.Matricula));
            operador.Notifications.Should().Contain(n => n.Property == nameof(Operador.Nome));
        }

        [Fact]
        public void CriarOperador_OperadorValido_Teste()
        {
            var operador = new Operador(
                new Matricula("123456"),
                new Nome("João", "Silva")
            );

            operador.Valid.Should().BeTrue();
        }

        [Fact]
        public void CriarOperador_VarificacaoCampos_Teste()
        {
            var operador = new Operador(
                new Matricula("123456"),
                new Nome("João", "Silva")
            );

            operador.Nome.PrimeiroNome.Should().Be("João");
            operador.Nome.Sobrenome.Should().Be("Silva");
            operador.Matricula.Numero.Should().Be("123456");
        }
    }
}
