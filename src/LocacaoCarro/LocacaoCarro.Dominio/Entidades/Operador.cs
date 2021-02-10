using Flunt.Validations;

namespace LocacaoCarro.Dominio.Entidades
{
    public class Operador : Usuario
    {
        public Operador(Matricula matricula, Nome nome)
            : base(nome)
        {
            Matricula = matricula;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Matricula, nameof(Matricula), "Matricula não pode ser nula"));

            if (Nome != null)
                AddNotifications(Nome);

            if (Matricula != null)
                AddNotifications(Matricula);

        }

        public Matricula Matricula { get; private set; }
    }
}
