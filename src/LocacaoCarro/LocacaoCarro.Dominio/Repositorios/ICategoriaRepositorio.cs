using LocacaoCarro.Dominio.Entidades.Veiculos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface ICategoriaRepositorio
    {
        Task<IEnumerable<Categoria>> ListarAsync();
    }
}
