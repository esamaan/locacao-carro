namespace LocacaoCarro.Infra.BDModelos
{
    public class ModeloBDModelo
    {
        public int Identificador { get; private set; }
        public string Descricao { get; private set; }
        public int LitrosBagageiro { get; private set; }
        public int NumeroOcupantes { get; private set; }
        public int AnoModelo { get; private set; }

        public int IdentificadorMarca { get; private set; }
        public string NomeMarca { get; private set; }

        public int IdentificadorCombustivel { get; private set; }
        public string NomeCombustivel { get; private set; }

        public int IdentificadorCategoria { get; set; }
        public string DescricaoCategoria { get; set; }
        public int PrecoCategoria { get; set; }
    }
}
