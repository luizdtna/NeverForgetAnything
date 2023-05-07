using Application.Interfaces;
using Application.Model;
using Domain.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]

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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemResponseDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        public async Task<IActionResult> Post(ItemDTO itemDTO)
        {
            Result resultInsert = await _itemApplication.InserirAsync(itemDTO);

            if (resultInsert.EhSucesso)
                return Created("", itemDTO);
            return BadRequest(resultInsert.Erro);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [Route("/{idItem}")]
        public async Task<IActionResult> Update([FromRoute] int idItem, ItemDTO itemDTO)
        {
            Result resultUpdate = await _itemApplication.AtualizarAsync(idItem, itemDTO);

            if (resultUpdate.EhSucesso)
                return NoContent();
            return BadRequest(resultUpdate.Erro);
        }

    }
}