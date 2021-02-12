using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Aplicacao.Modelos;
using LocacaoCarro.Aplicacao.Modelos.Veiculos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocacaoCarro.Api.Controllers
{
    /// <summary>
    /// Controller de veículos
    /// </summary>
    [ApiController]
    [Route("veiculos")]
    public class VeiculosController : Controller
    {
        private readonly IVeiculoAplicacao _veiculoAplicacao;

        /// <summary>
        /// Construtor da classe VeiculosController
        /// </summary>
        /// <param name="veiculoAplicacao">Application service que provê as operações de veículos.</param>
        public VeiculosController(IVeiculoAplicacao veiculoAplicacao)
        {
            _veiculoAplicacao = veiculoAplicacao;
        }

        /// <summary>
        /// Listagem de marcas
        /// </summary>
        /// <returns>Dados das marcas cadastradas</returns>
        [HttpGet]
        [Route("marcas")]
        [Authorize(Roles = "Cliente, Operador")]
        [ProducesResponseType(typeof(IEnumerable<MarcaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarMarcasAsync()
        {
            var resultado = await _veiculoAplicacao.ListarMarcasAsync();

            if (!resultado.Sucesso)
                return NotFound(resultado.Notifications);

            return Ok(resultado.Objeto);
        }

        /// <summary>
        /// Cadastrar uma nova marca
        /// </summary>
        [HttpPost]
        [Route("marcas")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarMarcaAsync([FromBody] MarcaModel marcaModel)
        {
            var resultado = await _veiculoAplicacao.CriarMarcaAsync(marcaModel);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return NoContent();
        }

        /// <summary>
        /// Listagem de modelos por categoria
        /// </summary>
        /// <returns>Dados das modelos cadastrados na categoria informada</returns>
        [HttpGet]
        [Route("modelos/{idCategoria}")]
        [Authorize(Roles = "Cliente, Operador")]
        [ProducesResponseType(typeof(IEnumerable<ModeloModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarModelosPorCategoriaAsync([FromRoute] int idCategoria)
        {
            var resultado = await _veiculoAplicacao.ListarModelosPorCategoriaAsync(idCategoria);

            if (!resultado.Sucesso)
                return NotFound(resultado.Notifications);

            return Ok(resultado.Objeto);
        }

        /// <summary>
        /// Cadastrar um novo modelo
        /// </summary>
        [HttpPost]
        [Route("modelos")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarModeloAsync([FromBody] ModeloModel modeloModel)
        {
            var resultado = await _veiculoAplicacao.CriarModeloAsync(modeloModel);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return NoContent();
        }

        /// <summary>
        /// Consultar um veículo por placa
        /// </summary>
        /// <param name="placa">Placa do veículo</param>
        /// <returns>Dados do veículo</returns>
        [HttpGet]
        [Route("{placa}")]
        [Authorize(Roles = "Cliente, Operador")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConsultarVeiculoPorPlacaAsync([FromRoute] string placa)
        {
            var resultado = await _veiculoAplicacao.ConsultarVeiculoPorPlacaAsync(placa);

            if (!resultado.Sucesso)
                return NotFound(resultado.Notifications);

            return Ok(resultado.Objeto);
        }

        /// <summary>
        /// Cadastrar um novo veículo
        /// </summary>
        /// <param name="veiculoModel">Dados do veículo</param>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarVeiculoAsync([FromBody] VeiculoModel veiculoModel)
        {
            var resultado = await _veiculoAplicacao.CriarVeiculoAsync(veiculoModel);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return NoContent();
        }

        /// <summary>
        /// Reservar veículo de determinado modelo
        /// </summary>
        /// <param name="idModelo">ID do modelo</param>
        /// <returns>Dados do veículo</returns>
        [HttpPost]
        [Route("reservar/{idModelo}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReservarVeiculoAsync([FromRoute] int idModelo)
        {
            var resultado = await _veiculoAplicacao.ReservarVeiculoPorModeloAsync(idModelo);

            if (!resultado.Sucesso)
                return UnprocessableEntity(resultado.Notifications);

            return Ok(resultado.Objeto);
        }

        /// <summary>
        /// Listagem de categorias
        /// </summary>
        /// <returns>Dados das categorias cadastradas</returns>
        [HttpGet]
        [Route("categorias")]
        [Authorize(Roles = "Cliente, Operador")]
        [ProducesResponseType(typeof(IEnumerable<CategoriaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErroModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarCategoriasAsync()
        {
            var resultado = await _veiculoAplicacao.ListarCategoriasAsync();

            if (!resultado.Sucesso)
                return NotFound(resultado.Notifications);

            return Ok(resultado.Objeto);
        }
    }
}
