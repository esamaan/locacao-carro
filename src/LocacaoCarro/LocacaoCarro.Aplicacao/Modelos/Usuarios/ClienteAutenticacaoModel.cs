namespace LocacaoCarro.Aplicacao.Modelos.Usuarios
{
    public class ClienteAutenticacaoModel
    {
        /// <summary>
        /// Dados do cliente
        /// </summary>
        public ClienteModel DadosCliente { get; set; }
        /// <summary>
        /// Token de autenticação (JWT)
        /// </summary>
        public string TokenAutenticacao { get; set; }
    }
}
