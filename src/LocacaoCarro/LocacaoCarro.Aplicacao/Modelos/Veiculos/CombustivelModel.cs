using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.ObjetosValor;
using System;

namespace LocacaoCarro.Aplicacao.Modelos.Veiculos
{
    public class CombustivelModel
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }

        public CombustivelModel() { }

        public CombustivelModel(Combustivel combustivel)
        {
            Identificador = combustivel.Identificador.Id;
            Nome = combustivel.Nome.Texto;
        }

        public Combustivel ToCombustivel()
        {
            return new Combustivel(new Identificador(Identificador), new Descricao(Nome));
        }
    }
}
