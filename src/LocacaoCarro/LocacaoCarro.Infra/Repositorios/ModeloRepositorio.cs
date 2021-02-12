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
    public class ModeloRepositorio : RepositorioBase<Modelo, ModeloBDModelo>, IModeloRepositorio
    {
        public ModeloRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public async Task Criar(Modelo modelo)
        {
            var query = @"
                INSERT INTO modelo (nome
                    , capacidade_bagageiro
                    , numero_ocupantes
                    , ano_modelo
                    , id_marca
                    , id_combustivel
                    , id_categoria)
                VALUES
                    (@nome
                    , @capacidade_bagageiro
                    , @numero_ocupantes
                    , @ano_modelo
                    , @id_marca
                    , @id_combustivel
                    , @id_categoria);";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@nome", modelo.Descricao.Texto, DbType.AnsiString);
            parametros.Add("@capacidade_bagageiro", modelo.LitrosBagageiro, DbType.Int32);
            parametros.Add("@numero_ocupantes", modelo.NumeroOcupantes, DbType.Int32);
            parametros.Add("@ano_modelo", modelo.AnoModelo, DbType.Int32);
            parametros.Add("@id_marca", modelo.Marca.Id, DbType.Int32);
            parametros.Add("@id_combustivel", modelo.Combustivel.Id, DbType.Int32);
            parametros.Add("@id_categoria", modelo.Categoria.Id, DbType.Int32);

            await ExecutarAsync(query, parametros);
        }

        public async Task<IEnumerable<Modelo>> Listar()
        {
            var query = @"
                SELECT m.id AS Identificador
                    , m.nome AS Descricao
                    , m.capacidade_bagageiro AS LitrosBagageiro
                    , m.numero_ocupantes AS NumeroOcupantes
                    , m.ano_modelo AS AnoModelo
                    , mm.id AS IdentificadorMarca
                    , mm.nome AS NomeMarca
                    , c.id AS IdentificadorCombustivel
                    , c.nome AS NomeCombustivel
                    , ca.id AS IdentificadorCategoria
                    , ca.descricao AS DescricaoCategoria
                    , ca.valor_hora AS PrecoCategoria
                FROM modelo m INNER
                    JOIN marca mm ON mm.id = m.id_marca INNER
                    JOIN combustivel c ON c.id = m.id_combustivel INNER
                    JOIN categoria ca ON ca.id = m.id_categoria;";

            return await BuscarListaAsync(query);
        }
    }
}
