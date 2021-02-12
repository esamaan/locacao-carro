using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Aplicacao.Resultados;
using System.Threading.Tasks;
using LocacaoCarro.Aplicacao.Modelos.Usuarios;

namespace LocacaoCarro.Aplicacao.Interfaces
{
    public interface IUsuarioApplicacao
    {
        Task<Resultado> CriarClienteAsync(ClienteModel clienteModel);
        Task<Resultado<ClienteModel>> ConsultarClienteAsync(string cpf);
        Task<Resultado> AtualizarClienteAsync(string cpf, ClienteModel clienteModel);
        Task<Resultado> RemoverClienteAsync(string cpf);
        Task<Resultado<ClienteAutenticacaoModel>> AutenticarClienteAsync(AutenticacaoInputModel autenticacaoInputModel);
        Task<Resultado<OperadorModel>> ConsultarOperadorAsync(string matricula);
        Task<Resultado<OperadorAutenticacaoModel>> AutenticarOperadorAsync(AutenticacaoInputModel autenticacaoInputModel);
    }
}
