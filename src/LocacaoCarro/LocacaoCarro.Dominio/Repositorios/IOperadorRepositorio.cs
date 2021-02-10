using LocacaoCarro.Dominio.Entidades;

namespace LocacaoCarro.Dominio.Repositorios
{
    public interface IOperadorRepositorio
    {
        void Incluir(Operador operador);
        void Obter(string matricula);
        Operador Autenticar(string matricula, string hashSenha);
    }
}
