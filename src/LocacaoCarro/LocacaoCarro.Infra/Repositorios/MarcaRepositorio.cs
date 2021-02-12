using AutoMapper;
using Dapper;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Infra.BDModelos;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LocacaoCarro.Infra.Repositorios
{
    public class MarcaRepositorio : RepositorioBase<Marca, MarcaBDModelo>, IMarcaRepositorio
    {
        public MarcaRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public async Task CriarAsync(Marca marca)
        {
            var query = @"
                INSERT INTO marca (nome)
                VALUES (
                    @nome
                );
            ";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@nome", marca.Nome.Texto, DbType.AnsiString);

            await ExecutarAsync(query, parametros);
        }

        public async Task<IEnumerable<Marca>> ListarAsync()
        {
            var query = @"
                SELECT id AS Identificador
                    , nome AS Nome
                FROM marca";

            return await BuscarListaAsync(query);
        }
    }
}
