namespace MauiAppProjeto2.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        btnLogin.Clicked += btnLogin_Clicked;

 
        btnRegistrar.Clicked += btnRegistrar_Clicked;
    }


    private void btnLogin_Clicked(object sender, EventArgs e)
    {

        DisplayAlert("Login", "Lógica de login ainda não feita.", "OK");
    }

    // 5. MÉTODO PARA O BOTÃO "CADASTRAR-SE"
    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {

        Navigation.PushAsync(new RegisterPage());

	}
}