using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IVeiculoRepositorio
    {
        Task<Veiculo> ConsultarPorPlaca(string placa);
        Task AlterarSituacao(string placa, SituacaoVeiculo situacao);
        Task Criar(Veiculo veiculo);
    }
}
