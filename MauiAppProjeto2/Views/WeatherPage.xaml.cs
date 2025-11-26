using MauiAppProjeto2.Models;
using System.Text.Json; // Importante para ler a resposta

namespace MauiAppProjeto2.Views
{
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }

        private async void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            // 1. Verifica se digitou algo
            if (string.IsNullOrEmpty(txtCidade.Text))
            {
                await DisplayAlert("Erro", "Por favor, digite o nome de uma cidade.", "OK");
                return;
            }

            try
            {
                // 2. Prepara os dados 
                string cidade = txtCidade.Text;
                string chave = "6135072afe7f6cec1537d5cb08a5a1a2"; 

                string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}&lang=pt_br";

                // 3. Faz a chamada na internet
                using (HttpClient client = new HttpClient())
                {
                    var resposta = await client.GetAsync(url);

                    if (resposta.IsSuccessStatusCode)
                    {
                        // 4. Lê o texto JSON que chegou
                        string conteudoJson = await resposta.Content.ReadAsStringAsync();

                        // 5. Traduz o JSON para as classes C# que criamos (Models)
                        var dadosClima = JsonSerializer.Deserialize<WeatherResponse>(conteudoJson, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        // 6. Atualiza a tela com os dados
                        lblCidade.Text = $"Cidade: {dadosClima.name}";
                        lblTemperatura.Text = $"Temperatura: {dadosClima.main.temp:F1}°C";
                        lblDescricao.Text = $"Condição: {dadosClima.weather[0].description}";
                        lblData.Text = $"Data: {DateTime.Now:dd/MM/yyyy HH:mm}";

                        // 1. Recuperar o ID do usuário que está logado (pegamos do cofre)
                        string idString = await SecureStorage.Default.GetAsync("id_usuario_logado");

                        // Precisamos converter de texto para número
                        int idUsuario = Convert.ToInt32(idString);

                        // 2. Criar o objeto Histórico para salvar
                        Historico novoHistorico = new Historico
                        {
                            Cidade = dadosClima.name,
                            DataConsulta = DateTime.Now,
                            Temperatura = $"{dadosClima.main.temp:F1}°C",
                            Descricao = dadosClima.weather[0].description,
                            IdUsuario = idUsuario
                        };

                        // 3. Mandar para o Banco de Dados
                        await App.Database.AddHistoricoAsync(novoHistorico);

                        // (Opcional) Só para você saber que salvou
                        Console.WriteLine("Histórico Salvo com Sucesso!");
                    }
                    else
                    {
                        await DisplayAlert("Ops", "Cidade não encontrada. Verifique o nome.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Falha na conexão: " + ex.Message, "OK");
            }
        }
    }
}