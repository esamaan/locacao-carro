using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.ObjetosValor;
using System;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class ModeloModel
    {
        public int Identificador { get; set; }
        public string Descricao { get; set; }
        public int LitrosBagageiro { get; set; }
        public int NumeroOcupantes { get; set; }
        public int AnoModelo { get; set; }
        public MarcaModel Marca { get; set; }
        public CombustivelModel Combustivel { get; set; }
        public CategoriaModel Categoria { get; set; }

        public ModeloModel() { }

        public ModeloModel(Modelo modelo)
        {
            Identificador = modelo.Identificador.Id;
            Descricao = modelo.Descricao.Texto;
            LitrosBagageiro = modelo.LitrosBagageiro;
            NumeroOcupantes = modelo.NumeroOcupantes;
            AnoModelo = modelo.AnoModelo;
            Marca = new MarcaModel(modelo.Marca);
            Combustivel = new CombustivelModel(modelo.Combustivel);
            Categoria = new CategoriaModel(modelo.Categoria);
        }

        public Modelo ToModelo()
        {
            return new Modelo(
                new Identificador(Identificador),
                new Descricao(Descricao),
                LitrosBagageiro,
                NumeroOcupantes,
                AnoModelo,
                Marca?.ToMarca(),
                Combustivel?.ToCombustivel(),
                Categoria?.ToCategoria()
            );
        }
    }
}
