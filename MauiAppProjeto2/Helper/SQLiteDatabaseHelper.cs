using SQLite;
using MauiAppProjeto2.Models;

namespace MauiAppProjeto2.Helper
{
    public class SQLiteDatabaseHelper
    {

        readonly SQLiteAsyncConnection _db;


        public SQLiteDatabaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<User>().Wait();
            _db.CreateTableAsync<Historico>().Wait();
        }

        // Método para CADASTRAR um novo usuário
        public Task<int> RegisterUserAsync(User user)
        {
            return _db.InsertAsync(user);
        }

        // Método para TENTAR LOGAR
        public Task<User> GetUserAsync(string email, string senha)
        {
            // Tradução do comando:
            // "Vá na tabela User..."
            // "Onde (Where) o Email for igual ao email digitado..."
            // "E (&&) a Senha for igual a senha digitada..."
            // "Me dê o PRIMEIRO que encontrar (FirstOrDefaultAsync)."
            return _db.Table<User>()
                      .Where(u => u.Email == email && u.Senha == senha)
                      .FirstOrDefaultAsync();
        }

        //Novo método para salvar o histórico
        public Task<int> AddHistoricoAsync(Historico historico)
        {
            return _db.InsertAsync(historico);
        }

        // Vamos precisar desse método depois para a tela de listagem
        public Task<List<Historico>> GetHistoricoDoUsuario(int idUsuario)
        {
            // Pega todo o histórico DAQUELE usuário específico
            return _db.Table<Historico>()
                      .Where(h => h.IdUsuario == idUsuario)
                      .OrderByDescending(h => h.DataConsulta) // Mais recentes primeiro
                      .ToListAsync();
        }
        // Método novo para filtrar
        public Task<List<Historico>> GetHistoricoFiltradoAsync(int idUsuario, DateTime inicio, DateTime fim)
        {
            // O SQLite compara datas automaticamente.
            // Pedimos tudo onde a data é MAIOR que 'inicio' E MENOR que 'fim'
            return _db.Table<Historico>()
                      .Where(h => h.IdUsuario == idUsuario && h.DataConsulta >= inicio && h.DataConsulta <= fim)
                      .OrderByDescending(h => h.DataConsulta)
                      .ToListAsync();
        }
    }
}
