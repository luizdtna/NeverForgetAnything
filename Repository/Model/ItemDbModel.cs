using Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Model
{
    public class ItemDbModel
    {
        [Key]
        [Column("id_item")]
        public int IdItem { get; set; }
        [Column("nome_item")]
        public string NomeItem { get; set; }
        [Column("descricao_localizacao")]
        public string DescricaoDetalhada { get; set; }
        [Column("imagem")]
        public string? Imagem { get; set; }
        [Column("matricula_usuario_alteracao")]
        public string? MatriculaAlteracao { get; set; }
        [Column("dt_alteracao")]
        public DateTime DataAlteracao { get; set; }
    }
}
