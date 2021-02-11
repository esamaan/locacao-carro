using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocacaoCarro.Api.Controllers
{
    /// <summary>
    /// Controller de usuários
    /// </summary>
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ApiBaseController
    {
        private readonly IUsuarioApplicacao _usuarioAplicacao;

        /// <summary>
        /// Construtor da classe UsuariosController
        /// </summary>
        /// <param name="usuarioAplicacao">Application service que provê as operações de usuários.</param>
        public UsuariosController(IUsuarioApplicacao usuarioAplicacao)
        {
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
        public async Task<IActionResult> ConsultarClienteAsync([FromRoute] string cpf)
        {
            var resultado = await _usuarioAplicacao.ConsultarClienteAsync(cpf);

            if (!resultado.Sucesso)
                return NotFound(resultado.Notifications);

            return Ok(resultado.Objeto);
        }

        /// <summary>
        /// Cadastra cliente
        /// </summary>
        /// <param name="clienteModel">Dados do cliente a ser inserido</param>
        [HttpPost]
        [Route("clientes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarClienteAsync([FromBody] ClienteModel clienteModel)
        {
            var resultado = await _usuarioAplicacao.CriarClienteAsync(clienteModel);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return NoContent();
        }

        /// <summary>
        /// Atualiza dados de um cliente
        /// </summary>
        /// <param name="cpf">CPF do cliente a ser atualizado</param>
        /// <param name="clienteModel">Dados atualizados do cliente</param>
        [HttpPut]
        [Route("clientes/{cpf}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarClienteAsync([FromRoute] string cpf, [FromBody] ClienteModel clienteModel)
        {
            var resultado = await _usuarioAplicacao.AtualizarClienteAsync(cpf, clienteModel);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return NoContent();
        }

        /// <summary>
        /// Remove um cliente da base
        /// </summary>
        /// <param name="cpf">CPF do cliente a ser removido</param>
        [HttpDelete]
        [Route("clientes/{cpf}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoverClienteAsync([FromRoute] string cpf)
        {
            var resultado = await _usuarioAplicacao.RemoverClienteAsync(cpf);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return NoContent();
        }
    }
}
