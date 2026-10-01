using MAUI_Coopera.Models;
using System.Net.Http.Json;

namespace MAUI_Coopera;

public partial class InicioPage : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public InicioPage()
	{
		InitializeComponent();
		CarregarPickerEst();
		CarregarPickerServicos();
		CarregarLista();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        txt.Text = "";
        pckEstrela.SelectedItem = "";
        pckServico.SelectedItem = "";
    }

	private void CarregarPickerEst()
	{
		for (int i = 1; i<=5;i++)
		{
			pckEstrela.Items.Add(i.ToString());
		}
	}

	private async void CarregarPickerServicos()
	{
        try
        {

            var response = await http.GetFromJsonAsync<List<ServicoModel>>($"{url}/servico");

            foreach (var i in response)
			{
				pckServico.Items.Add(i.NomeServico);
			}

        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }

    private async void CarregarLista()
	{
		try
		{

			var response = await http.GetFromJsonAsync<List<UsuarioModel>>($"{url}/usuario");

			list.ItemsSource = response;

		} catch (Exception ex)
		{
			await DisplayAlertAsync("Erro", ex.Message, "Ok");
		}
	}

    private async void Filtro_Changed(object sender, EventArgs e)
    {
        try
        {
            var nome = txt.Text ?? "";
            var servico = pckServico.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(servico))
            {
                CarregarLista();
                return;
            }

            var response = await http.GetFromJsonAsync<List<UsuarioModel>>(
                $"{url}/usuario/list?nome={nome}&servico={servico}");

            list.ItemsSource = response;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var botao = (ImageButton)sender;
            var usuario = (UsuarioModel)botao.BindingContext;

            var tel = new string(usuario.Telefone.Where(char.IsDigit).ToArray());
            var telFinal = tel.StartsWith("55") ? tel : $"55{tel}";

            var mensagem = Uri.EscapeDataString("Olá, Coopera");

            await Launcher.Default.OpenAsync($"https://wa.me/{telFinal}?text={mensagem}");

        } catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {

            var botao = (Button)sender;
            var usuario = (UsuarioModel)botao.BindingContext;

            Preferences.Set("profissionalEmail", usuario.Email);
            Preferences.Set("profissionalTelefone", usuario.Telefone);

            await Navigation.PushAsync(new PerfilProfissionalPage());

        } catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }
}