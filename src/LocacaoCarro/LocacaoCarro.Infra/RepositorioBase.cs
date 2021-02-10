using AutoMapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LocacaoCarro.Dominio.Repositorios;

namespace LocacaoCarro.Infra
{
    class RepositorioBase
    {
    }
    public abstract class RepositorioBase<TEntity, TDbEntity> : IRepositorioBase<TEntity>
    {
        private readonly IDbConnection _conexao;
        protected readonly IMapper _mapeamento;

        protected RepositorioBase(IDbConnection conexao, IMapper mapeamento)
        {
            _conexao = conexao;
            _mapeamento = mapeamento;
        }

        public async Task<T> BuscarAsync<T>(string query)
        {
            try
            {
                _conexao.Open();
                return await _conexao.QueryFirstOrDefaultAsync<T>(query);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<T> BuscarAsync<T>(string query, DynamicParameters param)
        {
            try
            {
                _conexao.Open();
                return await _conexao.QueryFirstOrDefaultAsync<T>(query, param);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<TEntity> BuscarAsync(string query)
        {
            try
            {
                _conexao.Open();
                var entidadeDb = await _conexao.QueryFirstOrDefaultAsync<TDbEntity>(query);

                return _mapeamento.Map<TDbEntity, TEntity>(entidadeDb);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<TEntity> BuscarAsync(string query, DynamicParameters param)
        {
            try
            {
                _conexao.Open();
                var entidadeDb = await _conexao.QueryFirstOrDefaultAsync<TDbEntity>(query, param);
                return _mapeamento.Map<TDbEntity, TEntity>(entidadeDb);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<IEnumerable<TEntity>> BuscarListaAsync(string query)
        {
            try
            {
                _conexao.Open();
                var listaEntidadesBd = await _conexao.QueryAsync<TDbEntity>(query);

                return _mapeamento.Map<IEnumerable<TDbEntity>, IEnumerable<TEntity>>(listaEntidadesBd);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<IEnumerable<TEntity>> BuscarListaAsync(string query, DynamicParameters param)
        {
            try
            {
                _conexao.Open();
                var listaEntidadesBd = await _conexao.QueryAsync<TDbEntity>(query, param);

                return _mapeamento.Map<IEnumerable<TDbEntity>, IEnumerable<TEntity>>(listaEntidadesBd);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<int> ExecutarAsync(TEntity entity, string query)
        {
            try
            {
                _conexao.Open();
                var entidadeDb = _mapeamento.Map<TEntity, TDbEntity>(entity);

                return await _conexao.ExecuteAsync(query, entidadeDb);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<int> ExecutarAsync(string query, DynamicParameters param)
        {
            try
            {
                _conexao.Open();
                return await _conexao.ExecuteAsync(query, param);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<int> ExecutarAsync(string query)
        {
            try
            {
                _conexao.Open();
                return await _conexao.ExecuteAsync(query);
            }
            finally
            {
                _conexao.Close();
            }
        }
    }
}
