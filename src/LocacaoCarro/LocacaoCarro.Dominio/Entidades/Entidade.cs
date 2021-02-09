using Flunt.Notifications;
using System;

namespace LocacaoCarro.Dominio.Entidades
{
    public abstract class Entidade : Notifiable
    {
        private Guid _id;
        public virtual Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        protected Entidade() => Id = Guid.NewGuid();
    }
}
