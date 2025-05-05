using BoutiqueApp.Models;
using BoutiqueApp.ViewModels;

namespace BoutiqueApp.Views;

public partial class ProductDetailsPage : ContentPage
{
    readonly ProductDetailsViewModel _vm;
    readonly CartViewModel _cartVm;
    public ProductDetailsPage(Product selectedProduct, CartViewModel cartVm)
    {
        InitializeComponent();
        _vm = new ProductDetailsViewModel(selectedProduct);
        _cartVm = cartVm;
        BindingContext = _vm;
    }

    private void OnAddToCartClicked(object sender, EventArgs e)
    {
        _cartVm.AddToCart(_vm.Product);
        DisplayAlert("Košarica", "Proizvod je dodan u košaricu!", "OK");
    }
}
