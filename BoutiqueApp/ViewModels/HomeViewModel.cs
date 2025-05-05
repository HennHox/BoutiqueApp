using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BoutiqueApp.Models;

namespace BoutiqueApp.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Product> Products { get; set; }
    public ObservableCollection<Product> FilteredProducts { get; set; }

    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged();
            FilterProducts();
        }
    }

    public ICommand SortByPriceCommand { get; }

    public HomeViewModel()
    {
        Products = new ObservableCollection<Product>(GetSampleProducts());
        FilteredProducts = new ObservableCollection<Product>(Products);
        SortByPriceCommand = new Command(SortProductsByPrice);
    }

    public void FilterProducts()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredProducts = new ObservableCollection<Product>(Products);
        }
        else
        {
            var filtered = Products
                .Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredProducts = new ObservableCollection<Product>(filtered);
        }
        OnPropertyChanged(nameof(FilteredProducts));
    }

    public void SortProductsByPrice()
    {
        var sorted = FilteredProducts.OrderBy(p => p.Price).ToList();
        FilteredProducts = new ObservableCollection<Product>(sorted);
        OnPropertyChanged(nameof(FilteredProducts));
    }

    private List<Product> GetSampleProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Haljina", Description = "Lagana ljetna haljina", Price = 299.99m, ImageUrl = "dress1.jpg", Category = "Haljine" },
            new Product { Id = 2, Name = "Torba", Description = "Modna torba", Price = 499.99m, ImageUrl = "bag1.jpg", Category = "Dodaci" },
            new Product { Id = 3, Name = "Cipele", Description = "Elegantne cipele", Price = 699.99m, ImageUrl = "shoes1.jpg", Category = "Obuća" }
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
