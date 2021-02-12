namespace LocacaoCarro.Aplicacao.Modelos.Usuarios
{
    /// <summary>
    /// Endereço
    /// </summary>
    public class EnderecoModel
    {
        /// <summary>
        /// CEP - código de endereçamento postal
        /// </summary>
        public string Cep { get; set; }
        /// <summary>
        /// Logradouro (nome da rua, avenida, alameda, etc.)
        /// </summary>
        public string Logradouro { get; set; }
        /// <summary>
        /// Número do imóvel
        /// </summary>
        public string Numero { get; set; }
        /// <summary>
        /// Complemento (ap, bloco, andar, etc.)
        /// </summary>
        public string Complemento { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Sigla do estado
        /// </summary>
        public string Estado { get; set; }
    }
}
