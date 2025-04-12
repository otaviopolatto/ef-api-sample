using FinanceControl.Domain.Dtos.Tag;
using FinanceControl.Service.TagService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinanceControl.Controller
{
    [ApiController]    
    [SwaggerTag("Tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService)); ;
        }

        /// <summary>
        /// Retorna todas as tags cadastradas no sistema
        /// </summary>
        [ProducesResponseType(200)]
        [HttpGet("/api/v1/tags/all")]
        public async Task<IActionResult> GetTags()
        {
            var result = await _tagService.GetTags();

            return Ok(result);

        }

        /// <summary>
        /// Retorna as informações de uma tag específica
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("/api/v1/tags/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tagService.GetTagById(id);

            return result.Status != 200 ? NotFound() : Ok(result.Data);

        }

        /// <summary>
        /// Adiciona um novo registro de tag
        /// </summary>
        [HttpPost("/api/v1/tags")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var result = await _tagService.CreateTag(request);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

      

        



    }
}
