using Domain.Core;

namespace Application.Model
{
    public class ItemResponseDTO
    {
        public int IdItem { get; set; }
        public string NomeItem { get; set; }
        public string DescricaoDetalhada { get; set; }
        public string? Imagem { get; set; }
        public string? MatriculaAlteracao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
