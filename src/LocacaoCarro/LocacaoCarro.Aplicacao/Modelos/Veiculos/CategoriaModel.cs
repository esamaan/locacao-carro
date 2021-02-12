using LocacaoCarro.Dominio.Entidades.Veiculos;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class CategoriaModel
    {
        public int Identificador { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }

        public CategoriaModel(Categoria categoria)
        {
            Identificador = categoria.Identificador.Id;
            Descricao = categoria.Descricao.Texto;
            Preco = categoria.Preco.ValorDecimal();
        }
    }
}
