using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class VeiculoModel
    {
        public int Identificador { get; private set; }
        public string Placa { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int IdModelo { get; private set; }
        public SituacaoVeiculo Situacao { get; private set; }

        public VeiculoModel(Veiculo veiculo)
        {
            Identificador = veiculo.Identificador.Id;
            Placa = veiculo.Placa.Numero;
            AnoFabricacao = veiculo.AnoFabricacao;
            IdModelo = veiculo.IdModelo;
            Situacao = veiculo.Situacao;
        }
    }
}
