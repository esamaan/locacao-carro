using System;

namespace LocacaoCarro.Api.Modelos
{
    public class ClienteModel
    {
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public EnderecoModel Endereco { get; private set; }
        public DateTime Aniversario { get; private set; }
    }
}
