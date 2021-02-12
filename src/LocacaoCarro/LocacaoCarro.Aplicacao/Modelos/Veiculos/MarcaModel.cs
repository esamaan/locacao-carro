using LocacaoCarro.Dominio.Entidades.Veiculos;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class MarcaModel
    {
        public int Identificador { get; private set; }
        public string Nome { get; private set; }

        public MarcaModel(Marca marca)
        {
            Identificador = marca.Identificador.Id;
            Nome = marca.Nome.Texto;
        }
    }
}
