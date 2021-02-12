using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Modelos.Veiculos;
using LocacaoCarro.Aplicacao.Resultados;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoCarro.Aplicacao
{
    public class VeiculoAplicacao : IVeiculoAplicacao
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IMarcaRepositorio _marcaRepositorio;
        private readonly IModeloRepositorio _modeloRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public VeiculoAplicacao(ICategoriaRepositorio categoriaRepositorio, IMarcaRepositorio marcaRepositorio, IModeloRepositorio modeloRepositorio, IVeiculoRepositorio veiculoRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _marcaRepositorio = marcaRepositorio;
            _modeloRepositorio = modeloRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task<Resultado<VeiculoModel>> ConsultarVeiculoPorPlacaAsync(string placa)
        {
            var placaVeiculo = new Placa(placa);

            var veiculo = await _veiculoRepositorio.ConsultarPorPlaca(placaVeiculo.Numero);

            if (veiculo == null)
                return Resultado<VeiculoModel>.Erro(nameof(Veiculo), "Veiculo não encontrado");

            return Resultado<VeiculoModel>.Ok(new VeiculoModel(veiculo));
        }

        public async Task<Resultado> CriarMarcaAsync(MarcaModel marcaModel)
        {
            var marca = marcaModel.ToMarca();

            if (string.IsNullOrWhiteSpace(marca.Nome.Texto))
                return Resultado.Erro(nameof(Marca), "O nome da marca não pode ser vazio");

            await _marcaRepositorio.Criar(marca);

            return Resultado.Ok();
        }

        public async Task<Resultado> CriarModeloAsync(ModeloModel modeloModel)
        {
            var modelo = modeloModel.ToModelo();

            if (!modelo.Valid)
                return Resultado.Erro(modelo.Notifications);

            await _modeloRepositorio.Criar(modelo);

            return Resultado.Ok();
        }

        public async Task<Resultado> CriarVeiculoAsync(VeiculoModel veiculoModel)
        {
            var veiculo = veiculoModel.ToVeiculo();

            if (!veiculo.Valid)
                return Resultado.Erro(veiculo.Notifications);

            var veiculoExistente = await _veiculoRepositorio.ConsultarPorPlaca(veiculo.Placa.Numero);

            if (veiculoExistente != null)
                return Resultado.Erro(nameof(Veiculo), "Veículo já cadastrado");

            await _veiculoRepositorio.Criar(veiculo);

            return Resultado.Ok();
        }

        public async Task<Resultado<IEnumerable<CategoriaModel>>> ListarCategoriasAsync()
        {
            var listaCategorias = await _categoriaRepositorio.Listar();

            return Resultado<IEnumerable<CategoriaModel>>.Ok(listaCategorias.Select(c => new CategoriaModel(c)));
        }

        public async Task<Resultado<IEnumerable<MarcaModel>>> ListarMarcasAsync()
        {
            var listaMarcas = await _marcaRepositorio.Listar();

            return Resultado<IEnumerable<MarcaModel>>.Ok(listaMarcas.Select(m => new MarcaModel(m)));
        }

        public async Task<Resultado<IEnumerable<ModeloModel>>> ListarModelosAsync()
        {
            var listaMarcas = await _modeloRepositorio.Listar();

            return Resultado<IEnumerable<ModeloModel>>.Ok(listaMarcas.Select(m => new ModeloModel(m)));
        }

        public async Task<Resultado<VeiculoModel>> ReservarVeiculoPorPlacaAsync(int idModelo)
        {
            var listaVeiculos = await _veiculoRepositorio.ListarPorModelo(idModelo);

            if (listaVeiculos == null || !listaVeiculos.Any())
                return Resultado<VeiculoModel>.Erro(nameof(Veiculo), "Nenhum veículo do modelo escolhido disponível");

            var veiculoSelecionado = listaVeiculos.First();

            await _veiculoRepositorio.AlterarSituacao(veiculoSelecionado.Placa.Numero, SituacaoVeiculo.Alugado);

            return await ConsultarVeiculoPorPlacaAsync(veiculoSelecionado.Placa.Numero);
        }
    }
}
