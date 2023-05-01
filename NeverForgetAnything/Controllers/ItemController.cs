using Application.Interfaces;
using Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("itens")]
    public class ItemController : ControllerBase
    {
        private readonly IItemApplication _itemApplication;
        public ItemController(IItemApplication itemApplication)
        {
            _itemApplication = itemApplication;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Get()
        {
            var resultado = await _itemApplication.ListarAsync();

            if (resultado.EhSucesso && (resultado.Objeto != null && resultado.Objeto.Any()))
                    return Ok(resultado.Objeto);
            else if (resultado.EhSucesso)
                return NoContent();

            return BadRequest(resultado.Erro);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemResponseDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/{idItem}")]
        public async Task<IActionResult> Get(int idItem)
        {
            var resultado = await _itemApplication.ObterAsync(idItem);

            if (resultado.EhSucesso && resultado.Objeto != null)
                return Ok(resultado.Objeto);
            else if (resultado.EhSucesso)
                return NoContent();

            return BadRequest(resultado.Erro);
        }

    }
}