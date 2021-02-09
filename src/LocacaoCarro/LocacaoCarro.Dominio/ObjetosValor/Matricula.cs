using Flunt.Validations;


namespace LocacaoCarro.Dominio.Entidades
{
    public class Matricula : ObjetoValor
    {
        public Matricula(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(Matricula.Numero), "Matrícula não pode ser nula ou em branco")
                .IsDigit(Numero, nameof(Matricula.Numero), "Matrícula deve conter apenas números"));
        }

        public string Numero { get; private set; }

        public override string ToString()
        {
            return Numero;
        }
    }
}