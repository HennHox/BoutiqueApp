using BoutiqueApp.Models;

namespace BoutiqueApp.ViewModels;

public class ProductDetailsViewModel
{
    public Product Product { get; set; }

    public ProductDetailsViewModel(Product product)
    {
        Product = product;
    }
}

