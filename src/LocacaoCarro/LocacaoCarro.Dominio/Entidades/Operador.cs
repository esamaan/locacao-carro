using Flunt.Validations;

namespace LocacaoCarro.Dominio.Entidades
{
    public class Operador : Usuario
    {
        public Matricula Matricula { get; private set; }

        public Operador(Matricula matricula, Nome nome, string hashSenha)
            : base(nome, hashSenha)
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

        public Operador(Matricula matricula, Nome nome)
            : this(matricula, nome, string.Empty)
        {

        }
        
    }
}
