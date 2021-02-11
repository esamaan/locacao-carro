using AutoMapper;
using LocacaoCarro.Api.Modelos;
using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocacaoCarro.Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioApplicacao _usuarioAplicacao;

        public UsuariosController(IMapper mapper, IUsuarioApplicacao usuarioAplicacao)
        {
            _mapper = mapper;
            _usuarioAplicacao = usuarioAplicacao;
        }

        /// <summary>
        /// Consulta cliente
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>Dados do cliente</returns>
        [HttpGet]
        [Route("clientes/{cpf}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterCliente([FromRoute] string cpf)
        {
            var resultado = await _usuarioAplicacao.ObterClienteAsync(new Cpf(cpf));

            if (!resultado.Sucesso)
                return NotFound(resultado.Notifications);

            return Ok(_mapper.Map<Cliente, ClienteModel>(resultado.Objeto));
        }
    }
}
