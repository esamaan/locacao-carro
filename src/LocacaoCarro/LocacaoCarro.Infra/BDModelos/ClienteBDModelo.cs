using System;

namespace LocacaoCarro.Infra.BDModelos
{
    public class ClienteBDModelo : UsuarioDBModelo
    {
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public DateTime Aniversario { get; set; }
    }
}
