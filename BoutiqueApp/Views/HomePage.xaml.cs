using BoutiqueApp.Models;
using BoutiqueApp.ViewModels;
using System;
using Microsoft.Maui.Controls;

namespace BoutiqueApp.Views
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        


        public HomePage()
        {
            InitializeComponent();
            viewModel = BindingContext as HomeViewModel;
        }

        // Navigacija na detalje proizvoda
        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Product selectedProduct)
            {
                await Navigation.PushAsync(new ProductDetailsPage(selectedProduct, App.CartVM));


                ((CollectionView)sender).SelectedItem = null;
            }
        }

        // Sortiranje po cijeni
        private void OnSortClicked(object sender, EventArgs e)
        {
            viewModel.SortProductsByPrice();
        }

        // Pretraga proizvoda
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FilterProducts();
        }

        // Navigacija na košaricu
        private async void OnCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage());
        }
    }
}
