using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Aplicacao.Resultados;
using System.Threading.Tasks;

namespace LocacaoCarro.Aplicacao.Interfaces
{
    public interface IUsuarioApplicacao
    {
        Task<Resultado> CriarClienteAsync(Cliente cliente);
        Task<Resultado<Cliente>> ConsultarClienteAsync(Cpf cpf);
        Task<Resultado> AtualizarClienteAsync(Cpf cpf, Cliente cliente);
        Task<Resultado> RemoverClienteAsync(Cpf cpf);
    }
}
