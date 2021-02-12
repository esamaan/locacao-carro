using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IVeiculoRepositorio
    {
        Task<Veiculo> ConsultarPorPlacaAsync(string placa);
        Task AlterarSituacaoAsync(string placa, SituacaoVeiculo situacao);
        Task CriarAsync(Veiculo veiculo);
        Task<IEnumerable<Veiculo>> ListarPorModeloAsync(int idModelo);
    }
}
