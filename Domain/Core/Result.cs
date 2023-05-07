using FluentValidation.Results;

namespace Domain.Core
{
    public class Result<T> : Result
    {
        public T? Objeto { get; private set; }

        protected Result(bool sucesso, IEnumerable<ValidationFailure> erro, T? objeto = default) : base(sucesso, erro)   
        {
            EhSucesso = sucesso;
            Erro = erro;
            Objeto = objeto;
        }

        public static Result<T> Ok(T objeto) 
        {
            return new Result<T>(true, Enumerable.Empty<ValidationFailure>(), objeto);
        }
        public static new Result<T> Ok()
        {
            return new Result<T>(true, Enumerable.Empty<ValidationFailure>(), default);
        }
        public static new Result<T> Error(List<ValidationFailure> erros)
        {
            return new Result<T>(false, erros);
        }
    }

    public class Result
    {
        public bool EhSucesso { get; protected set; }

        public IEnumerable<ValidationFailure> Erro { get; protected set; }

        protected Result(bool sucesso, IEnumerable<ValidationFailure> erro)
        {
            EhSucesso = sucesso;
            Erro = erro;
        }

        public static Result Ok()
        {
            return new Result(true, Enumerable.Empty<ValidationFailure>());
        }
        public static Result Error(List<ValidationFailure> erros)
        {
            return new Result(false, erros);
        }
    }
}
