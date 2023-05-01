using Application.Interfaces;
using Application.Model;
using Domain.Core;
using Domain.Entidades;
using Domain.Interface;

namespace Application.Application
{
    public class ItemApplication : IItemApplication
    {
        private readonly IItemRepository _itemRepository;
        public ItemApplication(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Result<IEnumerable<ItemResponseDTO>>> ListarAsync()
        {
            IEnumerable<ItemEntidade> listaItem = await _itemRepository.ListarItemAsync();

            IEnumerable<ItemResponseDTO> itemResponse = MapearEntidadeParaDTO(listaItem);
            return Result<IEnumerable<ItemResponseDTO>>.Ok(itemResponse);
        }

        private IEnumerable<ItemResponseDTO> MapearEntidadeParaDTO(IEnumerable<ItemEntidade> itensEntidade)
        {
            foreach (var itemEntidade in itensEntidade)
            {
                yield return new ItemResponseDTO
                {
                    IdItem = itemEntidade.IdItem,
                    NomeItem = itemEntidade.NomeItem,
                    DescricaoDetalhada = itemEntidade.DescricaoDetalhada,
                    Imagem = itemEntidade.Imagem,
                    MatriculaAlteracao = itemEntidade.MatriculaAlteracao,
                    DataAlteracao = itemEntidade.DataAlteracao
                };
            }
        }
    }
}