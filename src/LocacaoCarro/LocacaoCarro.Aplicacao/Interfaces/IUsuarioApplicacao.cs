using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Aplicacao.Resultados;
using System.Threading.Tasks;

namespace LocacaoCarro.Aplicacao.Interfaces
{
    public interface IUsuarioApplicacao
    {
        Task<Resultado> SalvarClienteAsync(Cliente cliente);
        Task<Resultado<Cliente>> ObterClienteAsync(string cpf);
        Task<Resultado> AtualizarClienteAsync(string cpf, Cliente cliente);
        Task<Resultado> RemoverClienteAsync(string cpf);
    }
}
