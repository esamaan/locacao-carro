using Flunt.Validations;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Dominio.Entidades.Veiculos
{
    public class Modelo : Entidade
    {
        public Identificador Identificador { get; private set; }
        public Descricao Descricao { get; private set; }
        public int LitrosBagageiro { get; private set; }
        public int NumeroOcupantes { get; private set; }
        public int AnoModelo { get; private set; }
        public Marca Marca { get; private set; }
        public Combustivel Combustivel { get; private set; }
        public Categoria Categoria { get; private set; }

        public Modelo(Identificador identificador, Descricao descricao, int litrosBagageiro, int numeroOcupantes, int anoModelo, Marca marca, Combustivel combustivel, Categoria categoria)
        {
            Identificador = identificador;
            Descricao = descricao;
            LitrosBagageiro = litrosBagageiro;
            NumeroOcupantes = numeroOcupantes;
            AnoModelo = anoModelo;
            Marca = marca;
            Combustivel = combustivel;
            Categoria = categoria;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Identificador, nameof(Identificador), "Id do modelo não pode ser nulo")
                .IsNotNull(Descricao, nameof(Descricao), "Descricao do modelo não pode ser nulo")
                .IsLowerThan(LitrosBagageiro, 0, nameof(LitrosBagageiro), "Capacidade do bagageiro não pode ser negativa")
                .IsLowerThan(NumeroOcupantes, 0, nameof(NumeroOcupantes), "Número de ocupantes não pode ser negativo")
                .IsNotNull(Marca, nameof(Marca), "Marca do modelo não pode ser nulo")
                .IsNotNull(Combustivel, nameof(Combustivel), "Combustivel do modelo não pode ser nulo")
                .IsNotNull(Categoria, nameof(Categoria), "Categoria do modelo não pode ser nula"));

            if (Identificador != null)
                AddNotifications(Identificador);

            if (Descricao != null)
                AddNotifications(Descricao);

            if (Marca != null)
                AddNotifications(Marca);

            if (Combustivel != null)
                AddNotifications(Combustivel);

            if (Categoria != null)
                AddNotifications(Categoria);
        }


    }
}
