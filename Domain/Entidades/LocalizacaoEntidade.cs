using Domain.Core;

namespace Domain.Entidades
{
    public class LocalizacaoEntidade
    {
        public IdNumerico IdLocalizacao { get; init; }
        public string Nome { get; init; }
        public DateTime DataAlteracao { get; init; }
        public string? MatriculaAlteracao { get; init; }

        public LocalizacaoEntidade(int idLocalizacao, string nome, DateTime dataAlteracao, string? matriculaAlteracao)
        {
            IdLocalizacao = new IdNumerico(idLocalizacao);
            Nome = nome;
            DataAlteracao = dataAlteracao;
            MatriculaAlteracao = matriculaAlteracao;
        }
    }
}
