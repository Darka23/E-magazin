using E_magazin.Data.Models;
using E_magazin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Contracts
{
    public interface IProductServices
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Manufacturer> GetManufacturers();
        IEnumerable<UnitOfMeasure> GetUnitOfMeasures();
        Task AddProduct([FromForm] Product product);

        IEnumerable<ProductListViewModel> Products();

        ProductDetailsViewModel GetProductById(int id);

        Task<bool> EditProduct([FromForm] Product product);

        void DeleteProduct(int id);

        Task<Product?> ProductForPlaceholder(int id);

        Task BuyProduct(int id);
        Task SellProduct([FromForm] SoldProduct soldProduct, int id);
        Task RestockProduct([FromForm] RestockViewModel restockProduct);
        CashDesk GetCashDesk();
    }
}
