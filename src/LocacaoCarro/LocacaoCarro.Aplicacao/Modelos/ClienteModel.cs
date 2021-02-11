using System;

namespace LocacaoCarro.Aplicacao.Modelos
{
    /// <summary>
    /// Dados do cliente
    /// </summary>
    public class ClienteModel
    {
        /// <summary>
        /// CPF
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// Primeiro nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Sobrenome
        /// </summary>
        public string Sobrenome { get; set; }
        /// <summary>
        /// Endereço
        /// </summary>
        public EnderecoModel Endereco { get; set; }
        /// <summary>
        /// Data de aniversário
        /// </summary>
        public DateTime Aniversario { get; set; }
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }

        public ClienteModel()
        {
            Endereco = new EnderecoModel();
        }
    }
}
