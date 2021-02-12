namespace LocacaoCarro.Aplicacao.Modelos.Usuarios
{
    public class OperadorAutenticacaoModel
    {
        /// <summary>
        /// Dados do operador
        /// </summary>
        public OperadorModel DadosOperador { get; set; }
        /// <summary>
        /// Token de autenticação (JWT)
        /// </summary>
        public string TokenAutenticacao { get; set; }
    }
}
