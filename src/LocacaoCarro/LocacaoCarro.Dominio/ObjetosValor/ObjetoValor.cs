using Flunt.Notifications;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public abstract class ObjetoValor : Notifiable
    {
        public ObjetoValor GetCopy()
        {
            return MemberwiseClone() as ObjetoValor;
        }
    }
}
