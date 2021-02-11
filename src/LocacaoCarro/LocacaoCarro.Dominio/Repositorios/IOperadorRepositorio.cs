using LocacaoCarro.Dominio.Entidades.Usuarios;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IOperadorRepositorio
    {
        Task<Operador> Consultar(string matricula);
        Task<Operador> Consultar(string matricula, string hashSenha);
        Task Criar(Operador operador);
        Task Atualizar(string matricula, Operador operador);
    }
}
