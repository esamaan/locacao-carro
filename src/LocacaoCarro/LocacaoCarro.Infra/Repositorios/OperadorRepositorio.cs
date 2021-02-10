using AutoMapper;
using Dapper;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Infra.BDModelos;
using System.Data;
using System.Threading.Tasks;

namespace LocacaoCarro.Infra.Repositorios
{
    public class OperadorRepositorio : RepositorioBase<Operador, OperadorBDModelo>, IOperadorRepositorio
    {
        private readonly IMapper _mapper;

        public OperadorRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public Operador Autenticar(string matricula, string hashSenha)
        {
            throw new System.NotImplementedException();
        }

        public void Incluir(Operador operador)
        {
            throw new System.NotImplementedException();
        }

        public void Obter(string matricula)
        {
            throw new System.NotImplementedException();
        }

    }
}
