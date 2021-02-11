using Flunt.Notifications;
using System.Collections.Generic;

namespace LocacaoCarro.Api.Modelos
{
    public class ErroModel
    {
        public List<string> Erros { get; } = new List<string>();

        public ErroModel(string erro)
        {
            Erros.Add(erro);
        }

        public ErroModel(IEnumerable<string> erros)
        {
            Erros.AddRange(erros);
        }

        public ErroModel(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                Erros.Add(notification.Message);
            }
        }
    }
}
