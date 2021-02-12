using FluentAssertions;
using LocacaoCarro.Aplicacao;
using LocacaoCarro.Aplicacao.Modelos.Veiculos;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Testes.Fixture;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LocacaoCarro.Testes.Aplicacao
{
    public class VeiculoAplicacaoTest
    {
        private readonly Mock<ICategoriaRepositorio> _categoriaRepositorio;
        private readonly Mock<IMarcaRepositorio> _marcaRepositorio;
        private readonly Mock<IModeloRepositorio> _modeloRepositorio;
        private readonly Mock<IVeiculoRepositorio> _veiculoRepositorio;

        private readonly VeiculoAplicacao _veiculoAplicacao;

        public VeiculoAplicacaoTest()
        {
            _categoriaRepositorio = new Mock<ICategoriaRepositorio>();
            _marcaRepositorio = new Mock<IMarcaRepositorio>();
            _modeloRepositorio = new Mock<IModeloRepositorio>();
            _veiculoRepositorio = new Mock<IVeiculoRepositorio>();

            _veiculoAplicacao = new VeiculoAplicacao(_categoriaRepositorio.Object, _marcaRepositorio.Object, _modeloRepositorio.Object, _veiculoRepositorio.Object);
        }

        #region ConsultarVeiculoPorPlacaAsync

        [Fact]
        public async Task ConsultarVeiculoPorPlacaAsync_VeiculoNaoExiste_NotificacaoVeiculoNaoEncontrado()
        {
            _veiculoRepositorio.Setup(x => x.ConsultarPorPlaca(It.IsAny<string>())).Returns(Task.FromResult<Veiculo>(null));

            var resultado = await _veiculoAplicacao.ConsultarVeiculoPorPlacaAsync("ABC123");

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().Contain(n => n.Property == nameof(Veiculo));
        }

        [Fact]
        public async Task ConsultarVeiculoPorPlacaAsync_VeiculoExiste_Ok()
        {
            var veiculo = new Veiculo(
                new Identificador(1),
                new Placa("ABC1234"),
                2021,
                1,
                SituacaoVeiculo.Disponível
            );

            _veiculoRepositorio.Setup(x => x.ConsultarPorPlaca(It.IsAny<string>())).Returns(Task.FromResult(veiculo));

            var resultado = await _veiculoAplicacao.ConsultarVeiculoPorPlacaAsync("ABC123");

            resultado.Sucesso.Should().BeTrue();
            resultado.Notifications.Should().BeEmpty();
        }
        #endregion ConsultarVeiculoPorPlacaAsync

        #region CriarMarcaAsync

        [Fact]
        public async Task CriarMarcaAsync_MarcaInvalida_NotificacoesDadosInvalidos()
        {
            var marcaInvalida = new MarcaModel();

            var resultado = await _veiculoAplicacao.CriarMarcaAsync(marcaInvalida);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().NotBeEmpty();
        }

         [Fact]
        public async Task CriarMarcaAsync_Ok()
        {
            var marca = new MarcaModel
            {
                Nome = "Fiat"
            };
            _marcaRepositorio.Setup(x => x.Criar(It.IsAny<Marca>())).Verifiable();

            var resultado = await _veiculoAplicacao.CriarMarcaAsync(marca);

            resultado.Sucesso.Should().BeTrue();
            _marcaRepositorio.Verify();
        }

        #endregion CriarMarcaAsync

        #region CriarModeloAsync

        [Fact]
        public async Task CriarModeloAsync_ModeloInvalido_NotificacoesDadosInvalidos()
        {
            var modeloInvalido = new ModeloModel();

            var resultado = await _veiculoAplicacao.CriarModeloAsync(modeloInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CriarModeloAsync_Ok()
        {
            var Modelo = new ModeloModel
            {
                AnoModelo = 2021,
                Categoria = new CategoriaModel
                {
                    Identificador = 1
                },
                Combustivel = new CombustivelModel
                {
                    Identificador = 1
                },
                Descricao = "Palio",
                LitrosBagageiro = 240,
                Marca = new MarcaModel
                {
                    Identificador = 1
                },
                NumeroOcupantes = 4
            };

            _modeloRepositorio.Setup(x => x.Criar(It.IsAny<Modelo>())).Verifiable();

            var resultado = await _veiculoAplicacao.CriarModeloAsync(Modelo);

            resultado.Sucesso.Should().BeTrue();
            _modeloRepositorio.Verify();
        }

        #endregion CriarModeloAsync

        #region CriarVeiculoAsync

        [Fact]
        public async Task CriarVeiculoAsync_VeiculoInvalido_NotificacoesDadosInvalidos()
        {
            var veiculoInvalido = new VeiculoModel();

            var resultado = await _veiculoAplicacao.CriarVeiculoAsync(veiculoInvalido);

            resultado.Sucesso.Should().BeFalse();
            resultado.Notifications.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CriarVeiculoAsync_Ok()
        {
            var veiculo = new VeiculoModel
            {
                AnoFabricacao = 2021,
                IdModelo = 1,
                Placa = "ABC1234",
                Situacao = SituacaoVeiculo.Disponível
            };
            _veiculoRepositorio.Setup(x => x.Criar(It.IsAny<Veiculo>())).Verifiable();

            var resultado = await _veiculoAplicacao.CriarVeiculoAsync(veiculo);

            resultado.Sucesso.Should().BeTrue();
            _marcaRepositorio.Verify();
        }

        #endregion CriarVeiculoAsync
 
    }
}
