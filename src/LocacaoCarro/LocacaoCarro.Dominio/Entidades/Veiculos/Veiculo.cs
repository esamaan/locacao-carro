using Flunt.Validations;
using LocacaoCarro.Dominio.Enums;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Dominio.Entidades.Veiculos
{
    public class Veiculo : Entidade
    {
        public Identificador Identificador { get; private set; }
        public Placa Placa { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int IdModelo { get; private set; }
        public SituacaoVeiculo Situacao { get; private set; }

        public Veiculo(Identificador identificador, Placa placa, int anoFabricacao, int idModelo, SituacaoVeiculo situacao)
        {
            Identificador = identificador;
            Placa = placa;
            AnoFabricacao = anoFabricacao;
            IdModelo = idModelo;
            Situacao = situacao;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Identificador, nameof(Identificador), "Identificador do veículo não pode ser nulo")
                .IsNotNull(Placa, nameof(Placa), "Placa do veículo não pode ser nula")
                .IsGreaterThan(AnoFabricacao, 1900, nameof(AnoFabricacao), "Ano de fabricação deve ser maior que 1900.")
                .IsGreaterThan(IdModelo, 0, nameof(IdModelo), "Id do modelo deve ser maior que zero."));

            if (Identificador != null)
                AddNotifications(Identificador);

            if (Placa != null)
                AddNotifications(Placa);
        }
    }
}
