using FluentValidation.Results;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Core
{
    public class Result<T>
    {
        public bool EhSucesso { get; private set; }
        public T? Objeto { get; private set; }
        public IEnumerable<ValidationFailure> Erro { get; private set; }

        protected Result(bool sucesso, IEnumerable<ValidationFailure> erro, T? objeto = default) 
        {
            EhSucesso = sucesso;
            Erro = erro;
            Objeto = objeto;
        }

        public static Result<T> Ok(T objeto) 
        {
            return new Result<T>(true, Enumerable.Empty<ValidationFailure>(), objeto);
        }
        public static Result<T> Ok()
        {
            return new Result<T>(true, Enumerable.Empty<ValidationFailure>());
        }

        public static Result<T> Error(List<ValidationFailure> erros)
        {
            return new Result<T>(false, erros);
        }




    }
}
