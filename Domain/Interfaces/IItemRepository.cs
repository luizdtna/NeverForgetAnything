using Domain.Entidades;

namespace Domain.Interface
{
    public interface IItemRepository
    {
        public Task<IEnumerable<ItemEntidade>> ListarItemAsync();
    }
}
