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
        public OperadorRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public async Task<Operador> Consultar(string matricula)
        {
            var query = @"
                SELECT u.nome AS Nome
	                , u.sobrenome AS Sobrenome
	                , o.matricula AS Matricula
                FROM usuario u
                INNER JOIN operador o ON o.id_usuario = u.id
                WHERE o.matricula = @matricula
            ";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@matricula", matricula, DbType.AnsiString);

            return await BuscarAsync(query, parametros);
        }

        public async Task<Operador> Consultar(string matricula, string hashSenha)
        {
            var query = @"
                SELECT u.nome AS Nome
	                , u.sobrenome AS Sobrenome
	                , o.matricula AS Matricula
                FROM usuario u
                INNER JOIN operador o ON o.id_usuario = u.id
                WHERE o.matricula = @matricula
                    AND u.hash_senha = @hash_senha
            ";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@matricula", matricula, DbType.AnsiString);
            parametros.Add("@hash_senha", hashSenha, DbType.AnsiString);

            return await BuscarAsync(query, parametros);
        }

        public async Task Criar(Operador operador)
        {
            var query = @"
                BEGIN TRY
                    BEGIN TRANSACTION;

                    INSERT INTO usuario
                    (
                        nome, 
                        sobrenome, 
                        hash_senha
                    )
                    VALUES 
                    (
                        @nome, 
                        @sobrenome, 
                        @hash_senha
                    )

                    INSERT INTO operador
                    (
                        id_usuario,
                        matricula
                    )
                    VALUES 
                    (
                        (SELECT SCOPE_IDENTITY()),
                        @matricula
                    )

                    COMMIT;
                END TRY
                BEGIN CATCH
	                ROLLBACK
                    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
                    DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
                    DECLARE @ErrorState INT = ERROR_STATE()
                    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                END CATCH;
            ";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@nome", operador.Nome.PrimeiroNome, DbType.AnsiString);
            parametros.Add("@sobrenome", operador.Nome.Sobrenome, DbType.AnsiString);
            parametros.Add("@hash_senha", operador.HashSenha, DbType.AnsiString);
            parametros.Add("@matricula", operador.Matricula, DbType.AnsiString);

            await ExecutarAsync(query, parametros);
        }

        public async Task Atualizar(string matricula, Operador operador)
        {
            var query = @"
                UPDATE usuario
                SET 
                    nome = @nome, 
                    sobrenome = @sobrenome
                FROM usuario u
                INNER JOIN operador o ON o.id_usuario = u.id
                WHERE o.matricula = @matricula;
            ";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@nome", operador.Nome.PrimeiroNome, DbType.AnsiString);
            parametros.Add("@sobrenome", operador.Nome.Sobrenome, DbType.AnsiString);
            parametros.Add("@matricula", operador.Matricula, DbType.AnsiString);

            await ExecutarAsync(query, parametros);
        }

    }
}
