namespace LocacaoCarro.Dominio.Entidades
{
    public abstract class Usuario : Entidade
    {
        public Nome Nome { get; private set; }
        public string HashSenha { get; set; }

        protected Usuario(Nome nome, string hashSenha)
        {
            Nome = nome;
            HashSenha = hashSenha;
        }

        protected Usuario(Nome nome)
            : this(nome, string.Empty)
        {

        }
    }
}
