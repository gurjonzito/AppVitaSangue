using SQLite;
using PCLExt.FileStorage.Folders;

namespace AppVitaSangue.Services
{
    public class DatabaseService
    {
        public SQLiteConnection conexao;

        public SQLiteConnection GetConexao()
        {
            var folder = new LocalRootFolder();

            var file = folder.CreateFile("list", PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);

            return new SQLiteConnection(file.Path);
        }
    }
}
