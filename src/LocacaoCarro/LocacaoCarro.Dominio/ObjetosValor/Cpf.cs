using Flunt.Validations;


namespace LocacaoCarro.Dominio.Entidades
{
    public class Cpf : ObjetoValor
    {
        public Cpf(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(Matricula.Numero), "CPF não pode ser nulo ou branco")
                .HasLen(Numero, 11, nameof(Matricula.Numero), "CPF deve conter 11 posições")
                .IsDigit(Numero, nameof(Matricula.Numero), "CPF deve conter apenas números"));
        }

        public string Numero { get; private set; }

        public override string ToString()
        {
            return Numero;
        }
    }
}