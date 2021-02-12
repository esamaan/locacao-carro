using Flunt.Validations;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Dominio.Entidades.Veiculos
{
    public class Marca : Entidade
    {
        public Identificador Identificador { get; private set; }
        public Descricao Nome { get; private set; }

        public Marca(Identificador identificador, Descricao descricao)
        {
            Identificador = identificador;
            Nome = descricao;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Identificador, nameof(Identificador), "Id da marca não pode ser nulo")
                .IsNotNull(Nome, nameof(Nome), "Nome da marca não pode ser nulo"));

            if (Identificador != null)
                AddNotifications(Identificador);

            if (Nome != null)
                AddNotifications(Nome);

        }

        
    }
}
