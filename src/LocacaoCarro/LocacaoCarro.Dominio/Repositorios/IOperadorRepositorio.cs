using LocacaoCarro.Dominio.Entidades;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IOperadorRepositorio
    {
        Task<Operador> Obter(string matricula);
        Task<Operador> Obter(string matricula, string hashSenha);
        Task Incluir(Operador operador);
        Task Atualizar(string matricula, Operador operador);
    }
}
