using Domain.Core;
using FluentValidation;

namespace Domain.Entidades.Validators.Core
{
    public class IdNumericoValidator : AbstractValidator<IdNumerico>
    {
        public string valorMenorQueZero = "Id deve ser maior ou igual a 1";
        public IdNumericoValidator() 
        {
            RuleFor(id => id.Valor)
                .GreaterThan(0)
                .WithName(nameof(IdNumerico))
                .WithErrorCode("IdNumerico_erro_negativo")
                .WithMessage(valorMenorQueZero);
        }
    }
}
