using SQLite;

namespace AppVitaSangue.Models
{
    public class Doador
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string DataNasc { get; set; }
        public string TipoSangue { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Senha { get; set; }
        public string DirImagem { get; set; }
        public string DescricaoPerfil { get; set; }

    }
}
