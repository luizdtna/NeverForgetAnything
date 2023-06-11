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

        public async Task AtualizarItemAsync(ItemEntidade itemEntidade)
        {
            ItemDbModel? item = await _sqlConext.Itens.SingleOrDefaultAsync(obj => obj.IdItem == itemEntidade.IdItem.Valor);
            if (item is not null)
            {
                var itemDb = new
                {
                    IdItem = itemEntidade.IdItem.Valor,
                    NomeItem = itemEntidade.NomeItem,
                    DescricaoDetalhada = itemEntidade.DescricaoDetalhada,
                    Imagem = itemEntidade.Imagem,
                    MatriculaAlteracao = itemEntidade.MatriculaAlteracao,
                    DataAlteracao = itemEntidade.DataAlteracao,
                    IdLocalizacao = itemEntidade.IdLocalizacao,
                };

                _sqlConext.Entry(item).CurrentValues.SetValues(itemDb);
                _sqlConext.SaveChanges();
            }
        }

        public async Task<IEnumerable<ItemEntidade>> ListarItemAsync()
        {
            IEnumerable<ItemDbModel> itensModel = await _sqlConext.Itens.ToListAsync();
            return MapearDbModelParaEntidade(itensModel);
        }

        public async Task InserirItemAsync(ItemEntidade itemEntidade)
        {
            var itemDb = MapearEntidadeParaDbModel(itemEntidade);

            await _sqlConext.AddAsync(itemDb);
            await _sqlConext.SaveChangesAsync();
        }

        private IEnumerable<ItemEntidade> MapearDbModelParaEntidade(IEnumerable<ItemDbModel> itensModel)
        {
            foreach (var item in itensModel)
            {
                yield return new ItemEntidade(item.IdItem, item.NomeItem, item.DescricaoDetalhada, item.Imagem, item.MatriculaAlteracao, item.DataAlteracao);
            }
        }
        private ItemDbModel MapearEntidadeParaDbModel(ItemEntidade itemModel)
        {
            return new ItemDbModel 
                { 
                    DescricaoDetalhada = itemModel.DescricaoDetalhada,
                    Imagem = itemModel.Imagem,
                    MatriculaAlteracao = itemModel.MatriculaAlteracao,
                    NomeItem = itemModel.NomeItem,
                    DataAlteracao = DateTime.Now,
                    IdLocalizacao = 1,
                };
        }
    }
}