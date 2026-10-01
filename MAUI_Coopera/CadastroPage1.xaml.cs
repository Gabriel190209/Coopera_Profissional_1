namespace MAUI_Coopera;

public partial class CadastroPage1 : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public CadastroPage1()
	{
		InitializeComponent();
	}

    private async void btnProximo_Clicked(object sender, EventArgs e)
    {
		try
		{
			if (!string.IsNullOrEmpty(txtPass.Text) && !string.IsNullOrEmpty(txtRePass.Text))
            {
                if (txtPass.Text == txtRePass.Text)
                {
                    if (txtRePass.Text.Length >= 5)
                    {
                        Preferences.Set("userSenha", txtPass.Text);
                        await Navigation.PushAsync(new CadastroPage2());
                    } else
                    {
                        await DisplayAlertAsync("Alerta", "A senha deve conter no minímo 5 caractéres", "Ok");
                    }
                } else
                {
                    await DisplayAlertAsync("Alerta", "A senha repetida deve ser a mesma que a senha escolhida", "Ok");
                }
            }
            else
            {
                await DisplayAlertAsync("Alerta", "Não podem haver campos vazios", "Ok");
            }
        } catch (Exception ex)
		{
            await DisplayAlertAsync("Erro", ex.Message, "Voltar");
        }
    }
}