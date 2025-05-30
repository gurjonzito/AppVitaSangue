using SQLite;

namespace AppVitaSangue.Models
{
    [Table("Doacoes")]
    public class Doacao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed] 
        public int DoadorId { get; set; }

        public string Hospital { get; set; }
        public DateTime DataDoacao { get; set; }

        [Ignore]
        public string DataFormatada => DataDoacao.ToString("dd/MM/yyyy");
    }
}