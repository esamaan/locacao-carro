using Flunt.Notifications;

namespace LocacaoCarro.Dominio.Entidades
{
    public abstract class ObjetoValor : Notifiable
    {
        public ObjetoValor GetCopy()
        {
            return MemberwiseClone() as ObjetoValor;
        }
    }
}
