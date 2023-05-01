using Application.Interfaces;
using Application.Model;
using Domain.Core;
using Domain.Entidades;
using Domain.Entidades.Validators;
using Domain.Interface;
using FluentValidation;
using FluentValidation.Results;

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

            IEnumerable<ItemResponseDTO> itemResponse = MapearItemEntidadeParaDTO(listaItem);
            return Result<IEnumerable<ItemResponseDTO>>.Ok(itemResponse);
        }

        public async Task<Result<ItemResponseDTO>> ObterAsync(int idItem)
        {

            var itemEntidade = new ItemEntidade(idItem);
            var result = new ItemValidator().Validate(itemEntidade, options =>
            {
                options.IncludeProperties(x => x.IdItem);
            });

            if (result.IsValid is false)
                return Result<ItemResponseDTO>.Error(result.Errors);


            IEnumerable<ItemEntidade> listaItem = await _itemRepository.ListarItemAsync();
            var item = listaItem.FirstOrDefault(i => i.IdItem.Valor == idItem);

            if (item == null)
                return Result<ItemResponseDTO>.Ok();
            
            ItemResponseDTO itemResponse = MapearItemEntidadeParaDTO(item);
            return Result<ItemResponseDTO>.Ok(itemResponse);
        }

        private IEnumerable<ItemResponseDTO> MapearItemEntidadeParaDTO(IEnumerable<ItemEntidade> itensEntidade)
        {
            foreach (var itemEntidade in itensEntidade)
            {
                yield return new ItemResponseDTO
                {
                    IdItem = itemEntidade.IdItem.Valor,
                    NomeItem = itemEntidade.NomeItem,
                    DescricaoDetalhada = itemEntidade.DescricaoDetalhada,
                    Imagem = itemEntidade.Imagem,
                    MatriculaAlteracao = itemEntidade.MatriculaAlteracao,
                    DataAlteracao = itemEntidade.DataAlteracao
                };
            }
        }

        private ItemResponseDTO MapearItemEntidadeParaDTO(ItemEntidade itemEntidade)
        {
            return new ItemResponseDTO
                {
                    IdItem = itemEntidade.IdItem.Valor,
                    NomeItem = itemEntidade.NomeItem,
                    DescricaoDetalhada = itemEntidade.DescricaoDetalhada,
                    Imagem = itemEntidade.Imagem,
                    MatriculaAlteracao = itemEntidade.MatriculaAlteracao,
                    DataAlteracao = itemEntidade.DataAlteracao
                };
        }
    }
}