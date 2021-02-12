using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.ObjetosValor;
using System;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class MarcaModel
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }

        public MarcaModel()
        {
        }

        public MarcaModel(Marca marca)
        {
            Identificador = marca.Identificador.Id;
            Nome = marca.Nome.Texto;
        }

        public Marca ToMarca()
        {
            return new Marca(new Identificador(Identificador), new Descricao(Nome));
        }
    }
}
