using Domain.Entidades.Validators.Core;
using FluentValidation;
using Util;

namespace Domain.Entidades.Validators
{
    public class ItemValidator : AbstractValidator<ItemEntidade>
    {

        public ItemValidator()
        {
            RuleFor(item => item.IdItem).SetValidator(new IdNumericoValidator());

            Include(new ItemNomeValidator());

            RuleFor(item => item.DescricaoDetalhada.Length)
                .LessThanOrEqualTo(100)
                .WithName(nameof(ItemEntidade.DescricaoDetalhada))
                .WithErrorCode("DescricaoDetalhada_erro_caracter")
                .WithMessage(ConstantesError.ERRO_LIMITE_CARACTERES.Replace("{0}", nameof(ItemEntidade.DescricaoDetalhada)).Replace("{1}", "100"));

            RuleFor(item => item.MatriculaAlteracao)
                .MaximumLength(10)
                .When(item => item.MatriculaAlteracao != null)
                .WithName(nameof(ItemEntidade.MatriculaAlteracao))
                .WithErrorCode("MatriculaAlteracao_erro_caracter")
                .WithMessage(ConstantesError.ERRO_LIMITE_CARACTERES.Replace("{0}", nameof(ItemEntidade.MatriculaAlteracao)).Replace("{1}", "200"));

            RuleFor(item => item.Imagem)
                .MaximumLength(10)
                .When(item => item.Imagem != null)
                .WithName(nameof(ItemEntidade.Imagem))
                .WithErrorCode("Imagem_erro_caracter")
                .WithMessage(ConstantesError.ERRO_LIMITE_CARACTERES.Replace("{0}", nameof(ItemEntidade.Imagem)).Replace("{1}", "200"));

            RuleFor(item => item.DataAlteracao)
                .NotNull()
                .WithName(nameof(ItemEntidade.DataAlteracao))
                .WithErrorCode("Imagem_erro_naoNulo")
                .WithMessage(ConstantesError.ERRO_PROPRIEDADE_NULA.Replace("{0}", nameof(ItemEntidade.DataAlteracao)));
        }
    }

    public class ItemNomeValidator : AbstractValidator<ItemEntidade>
    {
        public ItemNomeValidator()
        {
            RuleFor(item => item.NomeItem.Length)
                .LessThanOrEqualTo(45)
                .WithName(nameof(ItemEntidade.NomeItem))
                .WithErrorCode("NomeItem_erro_caracter")
                .WithMessage(ConstantesError.ERRO_LIMITE_CARACTERES.Replace("{0}", nameof(ItemEntidade.NomeItem)).Replace("{1}", "45"));
        }
    }
}
