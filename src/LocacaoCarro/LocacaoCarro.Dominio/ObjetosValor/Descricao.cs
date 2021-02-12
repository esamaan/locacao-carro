using Flunt.Validations;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Descricao : ObjetoValor
    {
        public string Texto { get; private set; }

        public Descricao(string texto)
        {
            Texto = texto;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Texto, nameof(Texto), "Descrição não pode ser nula ou em branco"));
        }

        public override string ToString()
        {
            return Texto;
        }
    }
}
