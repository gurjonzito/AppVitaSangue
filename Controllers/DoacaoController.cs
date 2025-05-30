using SQLite;
using AppVitaSangue.Services;
using AppVitaSangue.Models;

namespace AppVitaSangue.Controllers
{
    public class DoacaoController
    {
        private SQLiteConnection _conexao;

        public DoacaoController()
        {
            _conexao = new DatabaseService().GetConexao();
            _conexao.CreateTable<Doacao>();
        }

        public bool RegistrarDoacao(Doacao doacao)
        {
            return _conexao.Insert(doacao) > 0;
        }

        public List<Doacao> ObterDoacoesPorDoador(int doadorId)
        {
            return _conexao.Table<Doacao>()
                          .Where(d => d.DoadorId == doadorId)
                          .OrderByDescending(d => d.DataDoacao)
                          .ToList();
        }

        public Doacao ObterUltimaDoacao(int doadorId)
        {
            return _conexao.Table<Doacao>()
                          .Where(d => d.DoadorId == doadorId)
                          .OrderByDescending(d => d.DataDoacao)
                          .FirstOrDefault();
        }
        public bool ExcluirDoacao(int id)
        {
            return _conexao.Delete<Doacao>(id) > 0;
        }
    }
}