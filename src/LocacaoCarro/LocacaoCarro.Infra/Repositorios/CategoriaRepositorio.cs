using AutoMapper;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Infra.BDModelos;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LocacaoCarro.Infra.Repositorios
{
    public class CategoriaRepositorio : RepositorioBase<Categoria, CategoriaBDModelo>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public async Task<IEnumerable<Categoria>> ListarAsync()
        {
            var query = @"
                SELECT id AS Identificador
                    , descricao AS Descricao
                    , valor_hora AS Preco
                FROM categoria";

            return await BuscarListaAsync(query);
        }
    }
}
