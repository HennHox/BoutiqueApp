using BoutiqueApp.Views;
using BoutiqueApp.ViewModels;
namespace BoutiqueApp
{
    public partial class App : Application
    {
        public static CartViewModel CartVM { get; private set; } = new CartViewModel();
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());

        }
    }
}
