using Domain.Entidades;
using Domain.Interface;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly SqlConext _sqlConext;
        public ItemRepository(SqlConext sqlConext)
        {
            _sqlConext = sqlConext;
        }

        public async Task<IEnumerable<ItemEntidade>> ListarItemAsync()
        {
            IEnumerable<ItemDbModel> itensModel = await _sqlConext.Item.ToListAsync();
            return MapearDbModelParaEntidade(itensModel); 
        }

        private IEnumerable<ItemEntidade> MapearDbModelParaEntidade(IEnumerable<ItemDbModel> itensModel)
        {
            foreach(var item in itensModel)
            {
                yield return new ItemEntidade(item.IdItem, item.NomeItem, item.DescricaoDetalhada, item.Imagem, item.MatriculaAlteracao, item.DataAlteracao);
            }
        }
    }
}