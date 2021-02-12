using AutoMapper;
using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Modelos.Usuarios;
using LocacaoCarro.Aplicacao.Resultados;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Entidades.Usuarios;
using LocacaoCarro.Dominio.Repositorios;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace LocacaoCarro.Aplicacao
{
    public class UsuarioApplicacao : IUsuarioApplicacao
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOperadorRepositorio _operadorRepositorio;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioApplicacao(IClienteRepositorio clienteRepositorio, IOperadorRepositorio operadorRepositorio, IMapper mapper, IConfiguration configuration)
        {
            _clienteRepositorio = clienteRepositorio;
            _operadorRepositorio = operadorRepositorio;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Resultado<ClienteModel>> ConsultarClienteAsync(string cpf)
        {
            var cpfObj = new Cpf(cpf);

            var cliente = await _clienteRepositorio.ConsultarAsync(cpfObj.Numero);

            if (cliente == null)
                return Resultado<ClienteModel>.Erro(nameof(Cliente), "Cliente não encontrado");

            return Resultado<ClienteModel>.Ok(_mapper.Map< Cliente, ClienteModel>(cliente));
        }

        public async Task<Resultado> CriarClienteAsync(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);

            if (!cliente.Valid)
                return Resultado.Erro(cliente.Notifications);

            if (string.IsNullOrWhiteSpace(clienteModel.Senha))
                return Resultado.Erro(nameof(Cliente), "Senha não pode ser vazia");

            var clienteExistente = await _clienteRepositorio.ConsultarAsync(cliente.Cpf.Numero);

            if (clienteExistente != null)
                return Resultado.Erro(nameof(Cliente), "Cliente já cadastrado");

            cliente.HashSenha = ObterHashSenha(clienteModel.Senha);

            await _clienteRepositorio.CriarAsync(cliente);

            return Resultado.Ok();
        }

        private string ObterHashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return string.Empty;

            using var algoritmoSHA256 = SHA256.Create();
            var hash = algoritmoSHA256.ComputeHash(Encoding.ASCII.GetBytes(senha));
            return Convert.ToBase64String((hash));
        }

        public async Task<Resultado> AtualizarClienteAsync(string cpf, ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            var cpfObj = new Cpf(cpf);

            if (!cliente.Valid)
                return Resultado.Erro(cliente.Notifications);

            var clienteExistente = await _clienteRepositorio.ConsultarAsync(cliente.Cpf.Numero);

            if (clienteExistente == null)
                return Resultado.Erro(nameof(Cliente), "Cliente não encontrado");

            await _clienteRepositorio.AtualizarAsync(cpfObj.Numero, cliente);

            return Resultado.Ok();
        }

        public async Task<Resultado> RemoverClienteAsync(string cpf)
        {
            var cpfObj = new Cpf(cpf);

            var clienteExistente = await _clienteRepositorio.ConsultarAsync(cpfObj.Numero);

            if (clienteExistente == null)
                return Resultado.Erro(nameof(Cliente), "Cliente não encontrado");

            await _clienteRepositorio.RemoverAsync(cpfObj.Numero);

            return Resultado.Ok();
        }

        public async Task<Resultado<ClienteAutenticacaoModel>> AutenticarClienteAsync(AutenticacaoInputModel autenticacaoInputModel)
        {
            var hashSenha = ObterHashSenha(autenticacaoInputModel.Senha);
            var cpf = new Cpf(autenticacaoInputModel.Login);
            var cliente = await _clienteRepositorio.ConsultarAsync(cpf.Numero, hashSenha);

            if (cliente == null)
                return Resultado<ClienteAutenticacaoModel>.Erro(nameof(Cliente), "Login e/ou senha inválidos.");

            var resultado = new ClienteAutenticacaoModel
            {
                DadosCliente = _mapper.Map<Cliente, ClienteModel>(cliente),
                TokenAutenticacao = GerarTokenJwt(cliente)
            };

            return Resultado<ClienteAutenticacaoModel>.Ok(resultado);
        }

        public async Task<Resultado<OperadorModel>> ConsultarOperadorAsync(string matricula)
        {
            var matriculaOperador = new Matricula(matricula);

            var operador = await _operadorRepositorio.ConsultarAsync(matriculaOperador.Numero);

            if (operador == null)
                return Resultado<OperadorModel>.Erro(nameof(Operador), "Operador não encontrado");

            return Resultado<OperadorModel>.Ok(_mapper.Map<Operador, OperadorModel>(operador));
        }

        public async Task<Resultado<OperadorAutenticacaoModel>> AutenticarOperadorAsync(AutenticacaoInputModel autenticacaoInputModel)
        {
            var hashSenha = ObterHashSenha(autenticacaoInputModel.Senha);
            var matricula = new Matricula(autenticacaoInputModel.Login);
            var operador = await _operadorRepositorio.ConsultarAsync(matricula.Numero, hashSenha);

            if (operador == null)
                return Resultado<OperadorAutenticacaoModel>.Erro(nameof(Operador), "Login e/ou senha inválidos.");

            var resultado = new OperadorAutenticacaoModel
            {
                DadosOperador = _mapper.Map<Operador, OperadorModel>(operador),
                TokenAutenticacao = GerarTokenJwt(operador)
            };

            return Resultado<OperadorAutenticacaoModel>.Ok(resultado);
        }

        private string GerarTokenJwt(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Autenticacao:SegredoTokenJWT"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.ObterNomeUsuario()),
                    new Claim(ClaimTypes.Role, usuario.ObterPerfil())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
