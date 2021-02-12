using LocacaoCarro.Dominio.Entidades.Veiculos;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class ModeloModel
    {
        public int Identificador { get; private set; }
        public string Descricao { get; private set; }
        public int LitrosBagageiro { get; private set; }
        public int NumeroOcupantes { get; private set; }
        public int AnoModelo { get; private set; }
        public MarcaModel Marca { get; private set; }
        public CombustivelModel Combustivel { get; private set; }
        public CategoriaModel Categoria { get; private set; }

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
    }
}
