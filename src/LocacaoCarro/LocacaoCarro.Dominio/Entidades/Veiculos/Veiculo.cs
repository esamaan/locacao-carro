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
        }
    }
}
