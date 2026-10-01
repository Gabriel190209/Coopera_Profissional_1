using MAUI_Coopera.Models;
using System.Net.Http.Json;

namespace MAUI_Coopera;

public partial class CadastroPage3 : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";

    private byte[] imagemBytes;

    public CadastroPage3()
	{
		InitializeComponent();
	}

    private async void btnFinalizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            UsuarioCadastroModel userModel = new UsuarioCadastroModel()
            {
                Nome = Preferences.Get("userNome", string.Empty),
                Email = Preferences.Get("userEmail", string.Empty),
                Senha = Preferences.Get("userSenha", string.Empty),
                Pin = Preferences.Get("userPin", 0),
                Funcao = "Cliente",
                FotoPerfil = imagemBytes
            };

            var response = await http.PostAsJsonAsync<UsuarioCadastroModel>($"{url}/usuario/cadastro", userModel);

            if (response.IsSuccessStatusCode)
            {
                Preferences.Clear();
                await DisplayAlertAsync("Sucesso", "Cadastro realizado com sucesso", "Ok");
                await Navigation.PushAsync(new MainPage(), false);
            }
            else
            {
                await DisplayAlertAsync("Alerta", "Usuário ja cadastrado", "Ok");
            }
        } catch (Exception ex)
        {
            await DisplayAlertAsync(ex.Message, "Erro", "Voltar");
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            UsuarioCadastroModel userModel = new UsuarioCadastroModel()
            {
                Nome = Preferences.Get("userNome", string.Empty),
                Email = Preferences.Get("userEmail", string.Empty),
                Senha = Preferences.Get("userSenha", string.Empty),
                Pin = Preferences.Get("userPin", 0),
                Funcao = "Cliente",
                FotoPerfil = imagemBytes
            };

            var response = await http.PostAsJsonAsync<UsuarioCadastroModel>($"{url}/usuario/cadastro", userModel);

            if (response.IsSuccessStatusCode)
            {
                Preferences.Clear();
                await DisplayAlertAsync("Sucesso", "Cadastro realizado com sucesso", "Ok");
                await Navigation.PushAsync(new MainPage(), false);
            }
            else
            {
                await DisplayAlertAsync("Alerta", "Usuário ja cadastrado", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(ex.Message, "Erro", "Voltar");
        }
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        try
        {
            var resultado = await MediaPicker.CapturePhotoAsync();

            if (resultado != null)
            {
                using var stream = await resultado.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                imagemBytes = memoryStream.ToArray();

                imgPerfil.Source = ImageSource.FromStream(() => new MemoryStream(imagemBytes));
            } else
            {
                imgPerfil.Source = ImageSource.FromFile("mais.png");
                //await DisplayAlertAsync("Selecione uma foto", "Alerta", "Voltar");
            }
        } catch (Exception ex)
        {
            lblAgoraNao.Text = ex.Message;
            await DisplayAlertAsync(ex.Message, "Erro", "Voltar");
        }
    }
}