using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.ObjetosValor;
using System;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class CategoriaModel
    {
        public int Identificador { get; set; }
        public string Descricao { get; set; }
        public double PrecoHora { get; set; }

        public CategoriaModel() { }

        public CategoriaModel(Categoria categoria)
        {
            Identificador = categoria.Identificador.Id;
            Descricao = categoria.Descricao.Texto;
            PrecoHora = categoria.PrecoHora.ValorDecimal();
        }

        public Categoria ToCategoria()
        {
            return new Categoria(new Identificador(Identificador), new Descricao(Descricao), new Preco(PrecoHora));
        }
    }
}
