using LocacaoCarro.Aplicacao.Modelos.Veiculos;
using LocacaoCarro.Aplicacao.Resultados;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Aplicacao.Interfaces
{
    public interface IVeiculoAplicacao
    {
        Task<Resultado<IEnumerable<MarcaModel>>> ListarMarcasAsync();
        Task<Resultado> CriarMarcaAsync(MarcaModel marcaModel);

        Task<Resultado<IEnumerable<ModeloModel>>> ListarModelosPorCategoriaAsync(int idCategoria);
        Task<Resultado> CriarModeloAsync(ModeloModel modeloModel);

        Task<Resultado<VeiculoModel>> ConsultarVeiculoPorPlacaAsync(string placa);
        Task<Resultado> CriarVeiculoAsync(VeiculoModel veiculoModel);
        Task<Resultado<VeiculoModel>> ReservarVeiculoPorModeloAsync(int idModelo);

        Task<Resultado<IEnumerable<CategoriaModel>>> ListarCategoriasAsync();
    }
}
