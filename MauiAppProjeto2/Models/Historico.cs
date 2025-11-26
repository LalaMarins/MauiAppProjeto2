using SQLite;

namespace MauiAppProjeto2.Models
{
    public class Historico
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Cidade { get; set; }
        public DateTime DataConsulta { get; set; }
        public string Temperatura { get; set; }
        public string Descricao { get; set; } // Ex: "céu limpo"

        // IMPORTANTE: Precisamos saber QUEM fez essa consulta
        public int IdUsuario { get; set; }
    }
}