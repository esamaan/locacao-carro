namespace LocacaoCarro.Dominio.Entidades
{
    public abstract class Usuario : Entidade
    {
        public Nome Nome { get; private set; }

        protected Usuario(Nome nome)
        {
            Nome = nome;
        }
    }
}
