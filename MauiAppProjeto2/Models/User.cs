using SQLite;

namespace MauiAppProjeto2.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
