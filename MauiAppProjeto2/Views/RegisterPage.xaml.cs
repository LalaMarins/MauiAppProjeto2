using MauiAppProjeto2.Models;
namespace MauiAppProjeto2.Views;
using System.ComponentModel.DataAnnotations; // Para usar o "Validator"
using System.Collections.Generic; // Para usar a List<ValidationResult>


public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();



        btnCadastrar.Clicked += btnCadastrar_Clicked;
    }

    private async void btnCadastrar_Clicked(object sender, EventArgs e)
    {
       
        try
        {

            User usuario = new User();


            usuario.Nome = txtNome.Text;
            usuario.DataNascimento = dpNascimento.Date;
            usuario.Email = txtEmail.Text;
            usuario.Senha = txtSenha.Text;


            var validationResults = new List<ValidationResult>();

           
            var validationContext = new ValidationContext(usuario);

            bool isValid = Validator.TryValidateObject(usuario, validationContext, validationResults, true);


            if (!isValid) 
            {

                string primeiroErro = validationResults.First().ErrorMessage;

                await DisplayAlert("Erro de Validação", primeiroErro, "OK");

               
                return;
            }


            await App.Database.RegisterUserAsync(usuario);

            await DisplayAlert("Sucesso!", "Usuário cadastrado.", "OK");
            //voltar pro login
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {

            await DisplayAlert("Ops...", ex.Message, "OK");
        }
    }
}
