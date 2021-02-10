using LocacaoCarro.Dominio.Entidades;
using System.Threading.Tasks;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IClienteRepositorio
    {
        Task Incluir(Cliente cliente);
        Task Obter(string cpf);
        Cliente Autenticar(string cpf, string hashSenha);
    }
}
