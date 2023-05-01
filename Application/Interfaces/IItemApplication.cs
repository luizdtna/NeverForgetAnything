using Application.Model;
using Domain.Core;
using Domain.Entidades;

namespace Application.Interfaces
{
    public interface IItemApplication
    {
        public Task<Result<IEnumerable<ItemResponseDTO>>> ListarAsync();
        public Task<Result<ItemResponseDTO>> ObterAsync(int idItem);
    }
}
