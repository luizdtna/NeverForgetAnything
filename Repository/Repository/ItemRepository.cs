using Domain.Entidades;
using Domain.Interface;

namespace Infrastructure.Repository
{
    public class ItemRepository : IItemRepository
    {
        public Task<IEnumerable<ItemEntidade>> ListarItemAsync()
        {
            throw new NotImplementedException();
        }
    }
}