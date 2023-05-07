using Domain.Entidades;

namespace Domain.Interface
{
    public interface IItemRepository
    {
        public Task<IEnumerable<ItemEntidade>> ListarItemAsync();
        public Task InserirItemAsync(ItemEntidade itemEntidade);
        public Task AtualizarItemAsync(ItemEntidade itemEntidade);
    }
}
