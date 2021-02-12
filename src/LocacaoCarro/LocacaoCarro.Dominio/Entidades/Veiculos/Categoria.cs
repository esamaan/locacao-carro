using Flunt.Validations;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Dominio.Entidades.Veiculos
{
    public class Categoria : Entidade
    {
        public Identificador Identificador { get; private set; }
        public Descricao Descricao { get; private set; }
        public Preco PrecoHora { get; private set; }

        public Categoria(Identificador identificador, Descricao descricao, Preco preco)
        {
            Identificador = identificador;
            Descricao = descricao;
            PrecoHora = preco;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Descricao, nameof(Descricao), "Descrição não pode ser nula")
                .IsNotNull(PrecoHora, nameof(PrecoHora), "Preço não pode ser nulo"));

            if (Identificador != null)
                AddNotifications(Identificador);

            if (Descricao != null)
                AddNotifications(Descricao);

            if (PrecoHora != null)
                AddNotifications(PrecoHora);

        }

        
    }
}
