namespace MauiAppProjeto2.Models
{
    // Esta classe representa a resposta inteira do site
    public class WeatherResponse
    {
        public MainInfo main { get; set; }      // Aqui vem temperatura, humidade...
        public WeatherInfo[] weather { get; set; } // Aqui vem a descrição (ex: "nublado")
        public string name { get; set; }        // O nome da cidade que ele encontrou
    }

    // Detalhes de temperatura
    public class MainInfo
    {
        public double temp { get; set; }
    }

    // Detalhes da condição visual
    public class WeatherInfo
    {
        public string description { get; set; } // Ex: "céu limpo", "chuva leve"
    }
}