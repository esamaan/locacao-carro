using Flunt.Validations;
using LocacaoCarro.Dominio.Entidades;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Endereco : ObjetoValor
    {
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public Endereco(string cep, string logradouro, string numero, string complemento, string cidade, string estado)
        {
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Cep, nameof(Cep), "CEP não pode ser nulo ou branco")
                .IsNotNullOrWhiteSpace(Logradouro, nameof(Logradouro), "Logradouro não pode ser nulo ou branco")
                .IsNotNullOrWhiteSpace(Cidade, nameof(Cidade), "Cidade não pode ser nulo ou branco")
                .IsNotNullOrWhiteSpace(Estado, nameof(Estado), "Estado não pode ser nulo ou branco"));
        }

        public override string ToString()
        {
            var enderecoComposto = $"{Logradouro}, {Numero}";

            if (!string.IsNullOrWhiteSpace(Complemento))
                enderecoComposto += $"/{Complemento}";

            enderecoComposto += $", {Cidade} - {Estado}. CEP: {Cep}";

            return enderecoComposto;
        }
    }
}
