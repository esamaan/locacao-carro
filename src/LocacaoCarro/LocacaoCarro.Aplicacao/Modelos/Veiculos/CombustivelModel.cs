using LocacaoCarro.Dominio.Entidades.Veiculos;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class CombustivelModel
    {
        public int Identificador { get; private set; }
        public string Nome { get; private set; }

        public CombustivelModel(Combustivel combustivel)
        {
            Identificador = combustivel.Identificador.Id;
            Nome = combustivel.Nome.Texto;
        }
    }
}
