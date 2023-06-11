using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    public class LocalizacaoDbModel
    {
        [Key]
        [Column("id_localizacao")]
        public int IdLocalizacao { get; set; }
        [Column("nome_localizacao")]
        public string NomeLocalizacao { get; set; }
        [Column("matricula_usuario_alteracao")]
        public string? MatriculaAlteracao { get; set; }
        [Column("dt_alteracao")]
        public DateTime DataAlteracao { get; set; }
    }
}
