using Flunt.Validations;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Placa : ObjetoValor
    {
        public string Numero { get; private set; }

        public Placa(string numero)
        {
            Numero = numero?.Trim();

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(Matricula.Numero), "Número da placa não pode ser nulo ou em branco"));
        }

        public override string ToString()
        {
            return Numero;
        }
    }
}