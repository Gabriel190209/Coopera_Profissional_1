using System.Net.Http.Json;

namespace MAUI_Coopera
{
    public partial class MainPage : ContentPage
    {
        private HttpClient http = new HttpClient();
        private readonly string url = "http://10.0.2.2:5218/api";
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new CadastroPage(), false);
        }

        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPass.Text))
                {
                    var response = await http.GetStringAsync($"{url}/usuario/login/{txtEmail.Text}/{txtPass.Text}");

                    if (!string.IsNullOrEmpty(response))
                    {
                        var responseFuncaoString = await http.GetStringAsync($"{url}/usuario/login/funcao/{txtEmail.Text}/{txtPass.Text}");
                        var responseFuncaoInt = responseFuncaoString.ToString().Trim();

                        var fotoResponse = await http.GetAsync($"{url}/usuario/login/foto/{txtEmail.Text}/{txtPass.Text}");
                        string fotoBase64 = string.Empty;

                        if (fotoResponse.IsSuccessStatusCode)
                        {
                            var fotoBytes = await fotoResponse.Content.ReadAsByteArrayAsync();
                            fotoBase64 = Convert.ToBase64String(fotoBytes);
                        }

                        Preferences.Set("userIdFuncao", responseFuncaoInt);
                        Preferences.Set("userNome", response);
                        Preferences.Set("userEmail", txtEmail.Text);
                        Preferences.Set("userFoto", fotoBase64); // fica vazio se não tiver foto
                        Preferences.Set("isLogged", true);
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        await DisplayAlertAsync("Alerta", "Não foi possivel realizar o login\n\nVerifique suas credenciais", "Voltar");
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

        private async void btnEsqueciSenha_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecuperarSenhaPage());
        }
    }
}
