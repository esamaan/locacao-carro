using AutoMapper;
using Dapper;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Infra.BDModelos;
using System.Data;
using System.Threading.Tasks;

namespace LocacaoCarro.Infra.Repositorios
{
    public class ClienteRepositorio : RepositorioBase<Cliente, ClienteBDModelo>, IClienteRepositorio
    {
        public ClienteRepositorio(IDbConnection conexao, IMapper mapeamento)
            : base(conexao, mapeamento)
        {

        }

        public Cliente Autenticar(string cpf, string hashSenha)
        {
            throw new System.NotImplementedException();
        }

        public async Task Incluir(Cliente cliente)
        {
            var query = @"
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

                INSERT INTO cliente
                (
                    cpf, 
                    cep, 
                    logradouro,
                    numero,
                    complemento,
                    cidade,
                    uf,
                    aniversario
                )
                VALUES 
                (
                    @cpf, 
                    @cep, 
                    @logradouro,
                    @numero,
                    @complemento,
                    @cidade,
                    @uf,
                    @aniversario
                )

                COMMIT;
            ";

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@nome", cliente.Nome.PrimeiroNome, DbType.AnsiString);
            parametros.Add("@sobrenome", cliente.Nome.Sobrenome, DbType.AnsiString);
            parametros.Add("@hash_senha", cliente.HashSenha, DbType.AnsiString);
            parametros.Add("@cpf", cliente.Cpf, DbType.AnsiString);
            parametros.Add("@cep", cliente.Endereco.Cep, DbType.AnsiString);
            parametros.Add("@logradouro", cliente.Endereco.Logradouro, DbType.AnsiString);
            parametros.Add("@numero", cliente.Endereco.Numero, DbType.AnsiString);
            parametros.Add("@complemento", cliente.Endereco.Complemento, DbType.AnsiString);
            parametros.Add("@cidade", cliente.Endereco.Cidade, DbType.AnsiString);
            parametros.Add("@uf", cliente.Endereco.Estado, DbType.AnsiString);
            parametros.Add("@aniversario", cliente.Aniversario, DbType.DateTime);

            await ExecutarAsync(query, parametros);
        }

        public async Task Obter(string cpf)
        {
            throw new System.NotImplementedException();
        }


    }
}
