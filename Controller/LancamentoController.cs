using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Dtos.Lancamentos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FinanceControl.Service.LancamentoService;

namespace FinanceControl.Controller
{
    [ApiController]
    [SwaggerTag("Lançamentos")]
    public class LancamentoController : ControllerBase
    {
        private readonly ILancamentoService _lancamentoService;

        public LancamentoController(ILancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService ?? throw new ArgumentNullException(nameof(lancamentoService)); ;
        }

        /// <summary>
        /// Retorna os registros de lançamentos cadastrados no sistema
        /// </summary>
        [ProducesResponseType(200)]
        [HttpGet("/api/v1/lancamentos/all")]
        public async Task<ActionResult<List<Lancamento>>> GetAll()
        {
            var response = await _lancamentoService.GetLancamentosService();

            return Ok(response);

        }

        /// <summary>
        /// Retorna as informações de um lançamento específico
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("/api/v1/lancamentos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _lancamentoService.GetLancamentoByIdService(id);

            return result.Status != 200 ? NotFound() : Ok(result.Data);

        }

        /// <summary>
        /// Adiciona um novo registro de lançamento
        /// </summary>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("/api/v1/lancamentos")]
        public async Task<ActionResult<Lancamento>> Create( [FromBody] CreateLancamentoRequest request)
        {

            var result = await _lancamentoService.CreateLancamentoService(request);

            return result.Status != 201 ? NotFound() : StatusCode(201, result.Data);

        }

        /// <summary>
        /// Adiciona um novo registro de lançamento
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPut("/api/v1/lancamentos")]
        public async Task<ActionResult<Lancamento>> Update( [FromBody] UpdateLancamentoRequest request)
        {

            var result = await _lancamentoService.UpdateLancamentoService(request);

            return result.Status switch
            {
                200 => Ok(result),
                400 => BadRequest(),
                404 => NotFound(),
                500 => StatusCode(StatusCodes.Status500InternalServerError, result),
                _ => BadRequest()
            };

        }

        /// <summary>
        /// Remove um registor de lançamento específico
        /// </summary>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("/api/v1/lancamentos/{id}")]
        public async Task<IActionResult> DeleteLancamento(int id)
        {
            var result = await _lancamentoService.DeleteLancamentoService(id);

            return result.Status != 204 ? NotFound() : StatusCode(204, result.Data);

        }


    }
}
