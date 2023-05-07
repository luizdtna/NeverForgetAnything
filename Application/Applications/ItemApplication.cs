using Application.Interfaces;
using Application.Model;
using Domain.Core;
using Domain.Entidades;
using Domain.Entidades.Validators;
using Domain.Interface;
using FluentValidation;

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

            IEnumerable<ItemResponseDTO> itemResponse = MapearListaEntidadeParaDTO(listaItem);
            return Result<IEnumerable<ItemResponseDTO>>.Ok(itemResponse);
        }

        public async Task<Result<ItemResponseDTO>> ObterAsync(int idItem)
        {

            var itemEntidade = new ItemEntidade() { IdItem = new IdNumerico(idItem) };
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
        public async Task<Result> InserirAsync(ItemDTO itemDTO)
        {
            ItemEntidade itemEntidade = MapearDTOParaEntidade(itemDTO);
            var resultValidation = new ItemValidator().Validate(itemEntidade, options =>
            {
                options.IncludeProperties(x => x.DescricaoDetalhada);
                options.IncludeProperties(x => x.MatriculaAlteracao);
                options.IncludeProperties(x => x.IdLocalizacao);
                options.IncludeProperties(x => x.Imagem);
            });

            if (resultValidation.IsValid)
            {
                await _itemRepository.InserirItemAsync(itemEntidade);
                return Result.Ok();
            }

            return Result.Error(resultValidation.Errors);
        }

        public async Task<Result> AtualizarAsync(int idItem, ItemDTO itemDTO)
        {

            var itemEntidade = MapearDTOParaEntidade(itemDTO, idItem);
            var resultValidation = new ItemValidator().Validate(itemEntidade);

            if (resultValidation.IsValid)
            {
                await _itemRepository.AtualizarItemAsync(itemEntidade);
                return Result.Ok();
            }

            return Result.Error(resultValidation.Errors);
        }

        private IEnumerable<ItemResponseDTO> MapearListaEntidadeParaDTO(IEnumerable<ItemEntidade> itensEntidade)
        {
            foreach (var itemEntidade in itensEntidade)
            {
                yield return MapearItemEntidadeParaDTO(itemEntidade);
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

        private ItemEntidade MapearDTOParaEntidade(ItemDTO itemResponseDTO, int? idItem = null)
        {
            return new ItemEntidade
            {
                IdItem = idItem.HasValue ? new IdNumerico(idItem.Value) : null,
                NomeItem = itemResponseDTO.NomeItem,
                DescricaoDetalhada = itemResponseDTO.DescricaoDetalhada,
                Imagem = itemResponseDTO.Imagem,
                MatriculaAlteracao = itemResponseDTO.MatriculaAlteracao,
                IdLocalizacao = itemResponseDTO.IdLocalizacao
            };
        }
    }
}