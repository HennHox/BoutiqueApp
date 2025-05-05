using BoutiqueApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BoutiqueApp.ViewModels;

public class CartViewModel : INotifyPropertyChanged
{
    public ObservableCollection<CartItem> CartItems { get; set; } = new();

    public decimal Total => CartItems.Sum(item => item.TotalPrice);

    public ICommand RemoveItemCommand { get; }
    public ICommand IncreaseQtyCommand { get; }
    public ICommand DecreaseQtyCommand { get; }

    public CartViewModel()
    {
        

        RemoveItemCommand = new Command<CartItem>(RemoveItem);
        IncreaseQtyCommand = new Command<CartItem>(IncreaseQty);
        DecreaseQtyCommand = new Command<CartItem>(DecreaseQty);
    }
    public void AddToCart(Product product)
    {
        var existingItem = CartItems.FirstOrDefault(i => i.Product.Name == product.Name); // Po nazivu, možeš promijeniti na ID ako postoji
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            CartItems.Add(new CartItem { Product = product, Quantity = 1 });
        }

        OnPropertyChanged(nameof(Total));
    }
    void RemoveItem(CartItem item)
    {
        if (CartItems.Contains(item))
        {
            CartItems.Remove(item);
            OnPropertyChanged(nameof(Total));
        }
    }

    void IncreaseQty(CartItem item)
    {
        item.Quantity++;
        OnPropertyChanged(nameof(Total));
    }

    void DecreaseQty(CartItem item)
    {
        if (item.Quantity > 1)
        {
            item.Quantity--;
            OnPropertyChanged(nameof(Total));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

