using MAUI_Coopera.Models;
using System.Net.Http.Json;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace MAUI_Coopera;

public partial class PerfilProfissionalPage : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public PerfilProfissionalPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CarregarDados();
    }

    private async void CarregarDados()
    {
        try
        {

            string email = Preferences.Get("profissionalEmail", string.Empty);

            var dados = await http.GetFromJsonAsync<PerfilProfissionalModel>($"{url}/usuario/profissional/dados/{email}");
            if (dados == null) return;

            lblNome.Text = dados.Nome;
            lblProfissao.Text = dados.Profissao;
            lblDescricao.Text = dados.Descricao;

            if (dados.FotoPerfil != null)
                imgPerfil.Source = ImageSource.FromStream(() => new MemoryStream(dados.FotoPerfil));

            listPortfolio.ItemsSource = dados.Portfolio
                .Select(bytes => ImageSource.FromStream(() => new MemoryStream(bytes)))
                .ToList();

        } catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }

    private async void btnWhats_Clicked(object sender, EventArgs e)
    {
        try
        {

            string telefone = Preferences.Get("profissionalTelefone", string.Empty);

            var tel = new string(telefone.Where(char.IsDigit).ToArray());
            var telFinal = tel.StartsWith("55") ? tel : $"55{tel}";

            var mensagem = Uri.EscapeDataString("Olá, Coopera");

            await Launcher.Default.OpenAsync($"https://wa.me/{telFinal}?text={mensagem}");

        } catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }
}