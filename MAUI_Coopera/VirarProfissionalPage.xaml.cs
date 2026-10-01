using MAUI_Coopera.Models;
using System.Net.Http.Json;

namespace MAUI_Coopera;

public partial class VirarProfissionalPage : ContentPage
{
    private HttpClient http = new HttpClient();
    private readonly string url = "http://10.0.2.2:5218/api";
    public VirarProfissionalPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarServicos();
    }

    private async void CarregarServicos()
    {
        try
        {

            var response = await http.GetFromJsonAsync<List<ServicoModel>>($"{url}/servico");

            foreach (var i in response)
            {
                pckServico.Items.Add(i.NomeServico);
            }

        } catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "Ok");
        }
    }
}