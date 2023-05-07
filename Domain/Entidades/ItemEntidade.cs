using Domain.Core;

namespace Domain.Entidades
{
    public class ItemEntidade
    {
        public IdNumerico? IdItem { get; init; }
        public string NomeItem { get; init; }
        public string DescricaoDetalhada { get; init; }
        public string? Imagem { get; init; }
        public string? MatriculaAlteracao { get; init; }
        public DateTime DataAlteracao { get; init; }
        public int IdLocalizacao { get; init; } 

        public ItemEntidade() { }
        public ItemEntidade(int idItem, string nomeItem, string descricaoDetalhada, string? imagem, string? matriculaAlteracao, DateTime dataAlteracao)
        {
            IdItem = new IdNumerico(idItem);
            NomeItem = nomeItem;
            DescricaoDetalhada = descricaoDetalhada;
            Imagem = imagem;
            MatriculaAlteracao = matriculaAlteracao;
            DataAlteracao = dataAlteracao;
        }
    }
}
