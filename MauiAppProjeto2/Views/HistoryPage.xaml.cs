using MauiAppProjeto2.Models;

namespace MauiAppProjeto2.Views
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            // Conecta os botões novos
            btnFiltrar.Clicked += BtnFiltrar_Clicked;
            btnLimpar.Clicked += BtnLimpar_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Quando abre a tela, carrega TUDO (limpa o filtro)
            BtnLimpar_Clicked(this, EventArgs.Empty);
        }

        // 1. Botão FILTRAR
        private async void BtnFiltrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string idString = await SecureStorage.Default.GetAsync("id_usuario_logado");
                int idUsuario = Convert.ToInt32(idString);

                // Pegamos as datas dos DatePickers
                DateTime dataInicio = dpInicio.Date;

                // Truque: Pegamos a data fim e somamos 1 dia (menos 1 segundo) 
                // para garantir que pegue o dia inteiro até as 23:59
                DateTime dataFim = dpFim.Date.AddDays(1).AddTicks(-1);

                // Chama o método NOVO do banco
                List<Historico> listaFiltrada = await App.Database.GetHistoricoFiltradoAsync(idUsuario, dataInicio, dataFim);

                listaHistorico.ItemsSource = listaFiltrada;

                if (listaFiltrada.Count == 0)
                    await DisplayAlert("Aviso", "Nenhuma consulta encontrada neste período.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        // 2. Botão LIMPAR (Mostra tudo de novo)
        private async void BtnLimpar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string idString = await SecureStorage.Default.GetAsync("id_usuario_logado");
                int idUsuario = Convert.ToInt32(idString);

                // Chama o método ANTIGO (que traz tudo)
                List<Historico> listaCompleta = await App.Database.GetHistoricoDoUsuario(idUsuario);

                listaHistorico.ItemsSource = listaCompleta;
            }
            catch (Exception ex)
            {
                // Ignora erros na inicialização
            }
        }
    }
}