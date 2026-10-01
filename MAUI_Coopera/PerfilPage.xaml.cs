using MAUI_Coopera.Models;
using System.Net.Http.Json;

namespace MAUI_Coopera;

public partial class PerfilPage : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public PerfilPage()
	{
		InitializeComponent();
		CarregarDados();
	}

	private void CarregarDados()
	{
		string fotoBase64 = Preferences.Get("userFoto", string.Empty);

		if (!string.IsNullOrEmpty(fotoBase64))
		{
            byte[] imagemBytes = Convert.FromBase64String(fotoBase64);
            imgPerfil.Source = ImageSource.FromStream(() => new MemoryStream(imagemBytes));
		} else
		{
            imgPerfil.Source = ImageSource.FromFile("mais.png");
        }

		lblOla.Text = $"Olá, {Preferences.Get("userNome", "Usuário")}";

		string funcao = Preferences.Get("userIdFuncao", "1");

		if (funcao == "1")
		{
			btnProfissional.Text = "Virar Profissional";
		} else
		{
            btnProfissional.Text = "Editar Perfil";
        }
    }

    private async void btnSair_Clicked(object sender, EventArgs e)
    {
		Preferences.Clear();
		Application.Current.MainPage = new NavigationPage(new MainPage());
    }

    private void btnProfissional_Clicked(object sender, EventArgs e)
    {
		if (btnProfissional.Text == "Virar Profissional")
		{
			Navigation.PushAsync(new VirarProfissionalPage());
		} else
		{
			DisplayAlert("a", "Fazer a pagina de editar perfil e também a parte da API", "Ok");
		}
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		try
		{

            var resultado = await MediaPicker.CapturePhotoAsync();

            if (resultado != null)
            {
                using var stream = await resultado.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var imagemBytes = memoryStream.ToArray();

                var u = new UsuarioAtualizarFotoPerfilModel()
                {
                    email = Preferences.Get("userEmail", string.Empty),SS
                    foto = imagemBytes
                };

                var response = await http.PutAsJsonAsync<UsuarioAtualizarFotoPerfilModel>($"{url}/usuario/atualizar/foto", u);

                if (response.IsSuccessStatusCode)
                {
                    imgPerfil.Source = ImageSource.FromStream(() => new MemoryStream(imagemBytes));
                } else
                {
                    await DisplayAlertAsync("Erro", "Não foi possivel adicionar foto de perfil", "Ok");
                    imgPerfil.Source = ImageSource.FromFile("mais.png");
                    return;
                }
            }
            else
            {
                imgPerfil.Source = ImageSource.FromFile("mais.png");
            }

        } catch (Exception ex)
		{
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }
}