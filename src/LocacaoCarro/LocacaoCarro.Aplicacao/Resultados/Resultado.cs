using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace LocacaoCarro.Aplicacao.Resultados
{
    public class Resultado : Notifiable
    {
        public bool Sucesso { get { return !Notifications.Any(); } }

        protected Resultado()
        {
        }

        protected Resultado(IReadOnlyCollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        public static Resultado Ok()
        {
            return new Resultado();
        }

        public static Resultado Erro(IReadOnlyCollection<Notification> notifications)
        {
            return new Resultado(notifications);
        }

        public static Resultado Erro(string propriedade, string notificacao)
        {
            return Erro(
                new Notification[]
                {
                    new Notification(propriedade, notificacao)
                }
            );
        }
    }

    public class Resultado<T> : Notifiable where T : class
    {
        public bool Sucesso { get { return !Notifications.Any(); } }
        public T Objeto { get; }

        private Resultado(T obj)
        {
            Objeto = obj;
        }

        private Resultado(IReadOnlyCollection<Notification> notifications)
        {
            Objeto = null;
            AddNotifications(notifications);
        }

        public static Resultado<T> Ok(T obj)
        {
            return new Resultado<T>(obj);
        }

        public static Resultado<T> Erro(IReadOnlyCollection<Notification> notifications)
        {
            return new Resultado<T>(notifications);
        }

        public static Resultado<T> Erro(string propriedade, string notificacao)
        {
            return Erro(
                new Notification[]
                {
                    new Notification(propriedade, notificacao)
                }
            );
        }
    }
}
