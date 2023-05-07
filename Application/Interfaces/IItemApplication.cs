using Application.Model;
using Domain.Core;

namespace Application.Interfaces
{
    public interface IItemApplication
    {
        public Task<Result<IEnumerable<ItemResponseDTO>>> ListarAsync();
        public Task<Result<ItemResponseDTO>> ObterAsync(int idItem);
        public Task<Result> InserirAsync(ItemDTO itemDTO);
        public Task<Result> AtualizarAsync(int idItem, ItemDTO itemDTO);
    }
}
