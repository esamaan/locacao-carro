using LocacaoCarro.Dominio.Entidades.Usuarios;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IOperadorRepositorio
    {
        Task<Operador> ConsultarAsync(string matricula);
        Task<Operador> ConsultarAsync(string matricula, string hashSenha);
        Task CriarAsync(Operador operador);
        Task AtualizarAsync(string matricula, Operador operador);
    }
}
