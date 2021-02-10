using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IRepositorioBase<TEntity>
    {
        Task<int> ExecutarAsync(string query);

        Task<int> ExecutarAsync(TEntity entity, string query);

        Task<TEntity> BuscarAsync(string query);

        Task<IEnumerable<TEntity>> BuscarListaAsync(string query);
    }
}
