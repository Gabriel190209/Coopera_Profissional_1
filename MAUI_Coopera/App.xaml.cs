using Microsoft.Extensions.DependencyInjection;

namespace MAUI_Coopera
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new NavigationPage(new MainPage()));
            bool logado = Preferences.Get("isLogged", false);

            if (logado == true)
            {
                return new Window(new AppShell());
            }
            else
            {
                return new Window(new NavigationPage(new MainPage()));
            }
            //return new Window(new AppShell());
        }
    }
}