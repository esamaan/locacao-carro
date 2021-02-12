using Flunt.Validations;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Dominio.Entidades.Veiculos
{
    public class Categoria : Entidade
    {
        public Identificador Identificador { get; private set; }
        public Descricao Descricao { get; private set; }
        public Preco Preco { get; private set; }

        public Categoria(Identificador identificador, Descricao descricao, Preco preco)
        {
            Identificador = identificador;
            Descricao = descricao;
            Preco = preco;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Identificador, nameof(Identificador), "Id não pode ser nulo")
                .IsNotNull(Descricao, nameof(Descricao), "Descrição não pode ser nula")
                .IsNotNull(Preco, nameof(Preco), "Preço não pode ser nulo"));

            if (Identificador != null)
                AddNotifications(Identificador);

            if (Descricao != null)
                AddNotifications(Descricao);

            if (Preco != null)
                AddNotifications(Preco);

        }

        
    }
}
