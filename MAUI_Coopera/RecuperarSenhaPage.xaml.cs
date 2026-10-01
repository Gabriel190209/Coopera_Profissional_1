namespace MAUI_Coopera;

public partial class RecuperarSenhaPage : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public RecuperarSenhaPage()
	{
		InitializeComponent();
	}

    private async void btnTrocar_Clicked(object sender, EventArgs e)
    {
		try
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPin.Text)
                && !string.IsNullOrEmpty(txtSenha.Text) && !string.IsNullOrEmpty(txtReSenha.Text))
            {

                if (txtReSenha.Text == txtSenha.Text)
                {

                    var response = await http.PutAsync($"{url}/usuario/atualizar/senha/{txtEmail.Text}/{txtPin.Text}/{txtSenha.Text}", null);

                    if (response.IsSuccessStatusCode)
                    {

                        await DisplayAlertAsync("Sucesso", "Senha atualizada com sucesso", "Ok");
                        await Navigation.PopAsync();

                    }
                    else
                    {
                        await DisplayAlertAsync("Alerta", "Não foi possivel trocar a senha\n\nVerifique suas credenciais e tente novamente", "Voltar");
                    }

                } else
                {
                    await DisplayAlertAsync("Alerta", "A senha repetida deve ser a mesma que a escolhida", "Ok");
                }

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