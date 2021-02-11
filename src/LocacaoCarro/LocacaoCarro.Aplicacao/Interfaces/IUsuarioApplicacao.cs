using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Aplicacao.Resultados;
using System.Threading.Tasks;
using LocacaoCarro.Aplicacao.Modelos;

namespace LocacaoCarro.Aplicacao.Interfaces
{
    public interface IUsuarioApplicacao
    {
        Task<Resultado> CriarClienteAsync(ClienteModel clienteModel);
        Task<Resultado<ClienteModel>> ConsultarClienteAsync(string cpf);
        Task<Resultado> AtualizarClienteAsync(string cpf, ClienteModel clienteModel);
        Task<Resultado> RemoverClienteAsync(string cpf);
        //Task<Resultado<Cliente>> AutenticarClienteAsync(Cpf cpf, string senha);
    }
}
