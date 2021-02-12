
namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Identificador : ObjetoValor
    {
        public int Id { get; private set; }

        public Identificador(int id)
        {
            Id = id;
        }
        
    }
}
