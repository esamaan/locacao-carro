using LocacaoCarro.Dominio.Entidades.Veiculos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IModeloRepositorio
    {
        Task<IEnumerable<Modelo>> ListarPorCategoriaAsync(int idCategoria);
        Task CriarAsync(Modelo modelo);
    }
}
