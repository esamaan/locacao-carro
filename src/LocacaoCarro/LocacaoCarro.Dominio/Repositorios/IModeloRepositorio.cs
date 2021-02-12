using LocacaoCarro.Dominio.Entidades.Veiculos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IModeloRepositorio
    {
        Task<IEnumerable<Modelo>> Listar();
        Task Criar(Modelo modelo);
    }
}
