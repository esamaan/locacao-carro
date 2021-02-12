

using Flunt.Validations;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Preco : ObjetoValor
    {
        public int Valor { get; private set; }

        public double ValorDecimal() => Valor / 100.0;

        public Preco(int valor)
        {
            Valor = valor;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(Valor, 0, nameof(Preco.Valor), "Preço não pode ser negativo"));
        }

        public Preco(double preco)
        {
            Valor = (int)(preco*100);
        }
    }
}
