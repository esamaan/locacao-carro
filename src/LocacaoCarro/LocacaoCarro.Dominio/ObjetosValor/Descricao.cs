using Flunt.Validations;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Descricao : ObjetoValor
    {
        public string Texto { get; private set; }

        public Descricao(string texto)
        {
            Texto = texto;
        }

        public override string ToString()
        {
            return Texto;
        }
    }
}
