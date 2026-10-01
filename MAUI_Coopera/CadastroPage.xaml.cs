namespace MAUI_Coopera;

public partial class CadastroPage : ContentPage
{
	private HttpClient http = new HttpClient();
	private readonly string url = "http://10.0.2.2:5218/api";

    public CadastroPage()
	{
		InitializeComponent();
	}

    private async void btnProximo_Clicked(object sender, EventArgs e)
    {
		try
		{
			if (!string.IsNullOrEmpty(txtEmail.Text))
			{
				Preferences.Set("userEmail", txtEmail.Text);
				await Navigation.PushAsync(new CadastroPage1());
			} else
			{
                await DisplayAlertAsync("Alerta", "Não podem haver campos vazios", "Ok");
            }
		} catch (Exception ex) 
		{
            await DisplayAlertAsync("Erro", ex.Message, "Voltar");
        }
    }
}