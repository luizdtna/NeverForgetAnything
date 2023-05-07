namespace Application.Model
{
    public class ItemDTO
    {
        public string NomeItem { get; set; }
        public string DescricaoDetalhada { get; set; }
        public string? Imagem { get; set; }
        public string? MatriculaAlteracao { get; set; }
        public int IdLocalizacao { get; set; }
    }
}
