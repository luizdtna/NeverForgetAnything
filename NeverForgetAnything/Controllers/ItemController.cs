using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Runtime.InteropServices;

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
        public IActionResult Get()
        {
            return BadRequest();
        }

        [HttpGet]
        [Route("/{idItem}")]
        public IActionResult Get(int idItem)
        {
            return BadRequest();
        }

    }
}