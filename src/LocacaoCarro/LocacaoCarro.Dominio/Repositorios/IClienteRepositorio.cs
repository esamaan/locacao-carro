using LocacaoCarro.Dominio.Entidades.Usuarios;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<Cliente> ConsultarAsync(string cpf);
        Task<Cliente> ConsultarAsync(string cpf, string hashSenha);
        Task CriarAsync(Cliente cliente);
        Task AtualizarAsync(string cpf, Cliente cliente);
        Task RemoverAsync(string cpf);
    }
}
