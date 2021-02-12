using Flunt.Validations;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Dominio.Entidades.Veiculos
{
    public class Combustivel : Entidade
    {
        public Identificador Identificador { get; private set; }
        public Descricao Nome { get; private set; }

        public Combustivel(Identificador identificador, Descricao nome)
        {
            Identificador = identificador;
            Nome = nome;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome do combustível não pode ser nulo"));

            if (Identificador != null)
                AddNotifications(Identificador);

            if (Nome != null)
                AddNotifications(Nome);

        }

        
    }
}
