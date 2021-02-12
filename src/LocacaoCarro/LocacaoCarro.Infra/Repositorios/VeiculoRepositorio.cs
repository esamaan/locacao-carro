using AutoMapper;
using Dapper;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Infra.BDModelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LocacaoCarro.Infra.Repositorios
{
    public class VeiculoRepositorio : RepositorioBase<Veiculo, VeiculoBDModelo>, IVeiculoRepositorio
    {
        public VeiculoRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public async Task AlterarSituacaoAsync(string placa, SituacaoVeiculo situacao)
        {
            var query = @"
                UPDATE veiculo
                    SET id_situacao = @id_situacao
                WHERE placa = @placa";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@placa", placa, DbType.AnsiString);
            parametros.Add("@id_situacao", (int)situacao, DbType.Int32);

            await ExecutarAsync(query, parametros);
        }

        public async Task<Veiculo> ConsultarPorPlacaAsync(string placa)
        {
            var query = @"
                SELECT id AS Identificador
	                , placa AS Placa
                    , ano_fabricacao AS AnoFabricacao
                    , id_modelo AS IdModelo
                    , id_situacao AS Situacao
                FROM veiculo
                WHERE placa = @placa";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@placa", placa, DbType.AnsiString);

            return await BuscarAsync(query, parametros);
        }

        public async Task CriarAsync(Veiculo veiculo)
        {
            var query = @"
                INSERT INTO veiculo (placa
                    , ano_fabricacao
                    , id_modelo
                    , id_situacao)
                VALUES
                    (@placa
                    , @ano_fabricacao
                    , @id_modelo
                    , @id_situacao)";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@placa", veiculo.Placa.Numero, DbType.AnsiString);
            parametros.Add("@ano_fabricacao", veiculo.AnoFabricacao, DbType.Int32);
            parametros.Add("@id_modelo", veiculo.IdModelo, DbType.Int32);
            parametros.Add("@id_situacao", (int)veiculo.Situacao, DbType.Int32);

            await ExecutarAsync(query, parametros);
        }

        public async Task<IEnumerable<Veiculo>> ListarPorModeloAsync(int idModelo)
        {
            var query = @"
                SELECT id AS Identificador
	                , placa AS Placa
                    , ano_fabricacao AS AnoFabricacao
                    , id_modelo AS IdModelo
                    , id_situacao AS Situacao
                FROM veiculo
                WHERE id_modelo = @id_modelo
                    AND id_situacao = @id_situacao";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@id_modelo", idModelo, DbType.Int32);
            parametros.Add("@id_situacao", (int)SituacaoVeiculo.Disponível, DbType.Int32);

            return await BuscarListaAsync(query, parametros);
        }
    }
}
