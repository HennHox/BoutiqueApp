using BoutiqueApp.Models;
using BoutiqueApp.ViewModels;
using Plugin.LocalNotification;

namespace BoutiqueApp.Views;

public partial class CartPage : ContentPage
{
    CartViewModel viewModel;

    public CartPage()
    {
        InitializeComponent();
        BindingContext = App.CartVM;
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.CommandParameter;

        bool confirm = await DisplayAlert("Potvrda", "Jeste li sigurni da želite obrisati proizvod iz košarice?", "Da", "Ne");
        if (confirm)
        {
            var viewModel = (CartViewModel)BindingContext;
            viewModel.RemoveItemCommand.Execute(item);
        }
    }


    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kupnja dovršena", "Hvala na kupnji!", "OK");
        ((CartViewModel)BindingContext).CartItems.Clear(); 
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        var viewModel = (CartViewModel)BindingContext;  // Uzimanje BindingContext-a iz page-a
        if (viewModel != null && viewModel.CartItems.Count > 0)
        {
            var request = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Ne zaboravi!",
                Description = "Imate proizvode u košarici – dovršite kupnju dok su još dostupni!",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(10)  // test: 10 sekundi kasnije
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }
    }


}
