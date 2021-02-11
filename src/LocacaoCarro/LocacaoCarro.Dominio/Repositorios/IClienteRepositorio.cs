using LocacaoCarro.Dominio.Entidades;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<Cliente> Consultar(string cpf);
        Task<Cliente> Consultar(string cpf, string hashSenha);
        Task Criar(Cliente cliente);
        Task Atualizar(string cpf, Cliente cliente);
        Task Remover(string cpf);
    }
}
