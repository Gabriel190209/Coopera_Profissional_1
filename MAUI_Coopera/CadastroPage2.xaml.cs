using MAUI_Coopera.Models;
using System.Net.Http.Json;

namespace MAUI_Coopera;

public partial class CadastroPage2 : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public CadastroPage2()
	{
		InitializeComponent();
	}

    private async void btnFinalizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtNome.Text) && !string.IsNullOrEmpty(txtPin.Text))
            {
                if (txtPin.Text.Length == 4)
                {
                    Preferences.Set("userPin",  Convert.ToInt32(txtPin.Text));
                    Preferences.Set("userNome", txtNome.Text);
                    await Navigation.PushAsync(new CadastroPage3());
                } else
                {
                    await DisplayAlertAsync("Alerta", "O PIN deve ter exatamente 4 digitos", "Ok");
                }
            }
            else
            {
                await DisplayAlertAsync("Alerta", "Não podem haver campos vazios", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Voltar");
        }
    }
}