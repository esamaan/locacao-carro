namespace LocacaoCarro.Dominio.Entidades
{
    public class Operador : Usuario
    {
        public Operador(Matricula matricula, Nome nome)
            : base(nome)
        {
            Matricula = matricula;
        }

        public Matricula Matricula { get; private set; }
    }
}
