using MauiAppProjeto2.Models;
using MauiAppProjeto2.Views;
namespace MauiAppProjeto2.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        btnLogin.Clicked += btnLogin_Clicked;

 
        btnRegistrar.Clicked += btnRegistrar_Clicked;
    }


    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        // 1. Verificação Simples: Campos vazios?
        if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text))
        {
            await DisplayAlert("Erro", "Preencha e-mail e senha.", "OK");
            return;
        }

        try
        {
            // 2. Pergunta ao Banco: "Existe alguém com esse e-mail e senha?"
            User usuarioEncontrado = await App.Database.GetUserAsync(txtEmail.Text, txtSenha.Text);

            // 3. Verificamos a resposta
            if (usuarioEncontrado != null)
            {
                // ACHOU! (Login Sucesso)

                // Dica: Aqui nós poderíamos salvar o ID do usuário para usar depois
                // Mas por enquanto, vamos só avisar.
                await DisplayAlert("Bem-vindo!", $"Olá, {usuarioEncontrado.Nome}", "OK");

                // Guardamos o ID no "cofre" do celular com o nome "id_usuario_logado"
                await SecureStorage.Default.SetAsync("id_usuario_logado", usuarioEncontrado.Id.ToString());

                // TODO: Navegar para a Tela Principal (Clima)
                Application.Current.MainPage = new AppShell();
                // Vamos fazer isso no próximo passo para organizar a "casa"
            }
            else
            {
                // NÃO ACHOU (Retornou null)
                await DisplayAlert("Falha", "E-mail ou senha incorretos.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    // 5. MÉTODO PARA O BOTÃO "CADASTRAR-SE"
    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new RegisterPage());

	}
}