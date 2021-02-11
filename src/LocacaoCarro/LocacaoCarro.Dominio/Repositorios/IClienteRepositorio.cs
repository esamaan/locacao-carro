using LocacaoCarro.Dominio.Entidades;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<Cliente> Obter(string cpf);
        Task<Cliente> Obter(string cpf, string hashSenha);
        Task Incluir(Cliente cliente);
        Task Atualizar(string cpf, Cliente cliente);
    }
}
