using Domain.Core;

namespace Domain.Entidades
{
    public class ItemEntidade
    {
        public IdNumerico IdItem { get; private set; }
        public string NomeItem { get; private set; }
        public string DescricaoDetalhada { get; private set; }
        public string? Imagem { get; private set; }
        public string? MatriculaAlteracao { get; private set; }
        public DateTime DataAlteracao { get; private set; }
        //public Localizacao Localizacao { get; set; } 

        public ItemEntidade(int idItem, string nomeItem, string descricaoDetalhada, string? imagem, string? matriculaAlteracao, DateTime dataAlteracao)
        {
            IdItem = new IdNumerico(idItem);
            NomeItem = nomeItem;
            DescricaoDetalhada = descricaoDetalhada;
            Imagem = imagem;
            MatriculaAlteracao = matriculaAlteracao;
            DataAlteracao = dataAlteracao;
        }

        public ItemEntidade(int idItem) => IdItem = new IdNumerico(idItem);
    }
}
