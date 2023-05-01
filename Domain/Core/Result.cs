namespace Domain.Core
{
    public class Result<T>
    {
        public bool EhSucesso { get; private set; }
        public T? Objeto { get; private set; }
        public string Erro { get; private set; }

        protected Result(bool sucesso, string erro, T? objeto = default) 
        {
            EhSucesso = sucesso;
            Erro = erro;
            Objeto = objeto;
        }

        public static Result<T> Ok(T objeto) 
        {
            return new Result<T>(true, string.Empty, objeto);
        }

        public static Result<T> Error(string erro)
        {
            return new Result<T>(false, erro);
        }




    }
}
