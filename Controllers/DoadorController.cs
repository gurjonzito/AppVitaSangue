using SQLite;
using AppVitaSangue.Services;
using AppVitaSangue.Models;

namespace AppVitaSangue.Controllers
{
    public class DoadorController
    {
        private DatabaseService databaseService;
        private SQLiteConnection conexao;

        public DoadorController()
        {
            databaseService = new DatabaseService();

            conexao = databaseService.GetConexao();

            conexao.CreateTable<Doador>();
        }

        public bool Insert(Doador value)
        {
            return conexao.Insert(value) > 0;
        }

        public bool Update(Doador value)
        {
            return conexao.Update(value) > 0;
        }

        public bool Delete(Doador value)
        {
            return conexao.Delete(value) > 0;
        }

        public List<Doador> GetAll()
        {
            return conexao.Table<Doador>().ToList();
        }

        public Doador GetById(int value)
        {
            return conexao.Find<Doador>(value);
        }

        public List<Doador> GetByNome(string value)
        {
            return conexao.Table<Doador>().Where(x => x.Nome.Contains(value)).ToList();
        }

        public Doador GetByCpf(string value)
        {
            return conexao.Table<Doador>().FirstOrDefault(d => d.CPF == value);
        }

        public bool ValidarLogin(string cpf, string senha)
        {
            var doador = GetByCpf(cpf);
            return doador != null && doador.Senha == senha;
        }
    }
}
