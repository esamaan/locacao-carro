using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;
using LocacaoCarro.Dominio.ObjetosValor;
using System;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class VeiculoModel
    {
        public int Identificador { get; set; }
        public string Placa { get; set; }
        public int AnoFabricacao { get; set; }
        public int IdModelo { get; set; }
        public SituacaoVeiculo Situacao { get; set; }

        public VeiculoModel() { }

        public VeiculoModel(Veiculo veiculo)
        {
            Identificador = veiculo.Identificador.Id;
            Placa = veiculo.Placa.Numero;
            AnoFabricacao = veiculo.AnoFabricacao;
            IdModelo = veiculo.IdModelo;
            Situacao = veiculo.Situacao;
        }

        public Veiculo ToVeiculo()
        {
            return new Veiculo(
                new Identificador(Identificador),
                new Placa(Placa),
                AnoFabricacao,
                IdModelo,
                Situacao
            );
        }
    }
}
