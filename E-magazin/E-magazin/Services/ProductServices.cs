using E_magazin.Contracts;
using E_magazin.Data.Models;
using E_magazin.Data.Repositories;
using E_magazin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Services
{
    public class ProductServices : IProductServices
    {
        private IApplicationDbRepository repo;

        public ProductServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public IEnumerable<Category> GetCategories()
        {
            return repo.All<Category>().ToList();
        }

        public async Task AddProduct([FromForm] Product product)
        {
            var existing = repo.All<Product>()
               .Where(r => r.Name == product.Name)
               .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("Product already exist");
            }

            await repo.AddAsync(new Product()
            {
                BuyingPrice = product.BuyingPrice,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                ManufacturerId = product.ManufacturerId,
                Manufacturer = product.Manufacturer,
                Name = product.Name,
                SellingPrice = product.SellingPrice,
                UnitOfMeasureId = product.UnitOfMeasureId,
                UnitOfMeasure = product.UnitOfMeasure,
                Description = product.Description,
                Amount = 0,
                IsSoldOut = true

            });

            await repo.SaveChangesAsync();
        }

        public IEnumerable<ProductListViewModel> Products()
        {
            return repo.All<Product>()
                .Select(p => new ProductListViewModel()
                {
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    Id = p.Id,
                    SellingPrice = p.SellingPrice,
                });
        }

        public ProductDetailsViewModel GetProductById(int id)
        {
            return repo.All<Product>()
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel()
                {
                    Id = p.Id,
                    BuyingPrice = p.BuyingPrice,
                    CategoryName = p.Category.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Manufacturer = p.Manufacturer,
                    ManufacturerName = p.Manufacturer.Name,
                    Name = p.Name,
                    SellingPrice = p.SellingPrice,
                    UnitOfMeasure = p.UnitOfMeasure,
                    UnitOfMeasureName = p.UnitOfMeasure.Name,
                    Amount = p.Amount,
                    IsSoldOut = p.IsSoldOut
                })
                .First();
        }

        public async Task<bool> EditProduct([FromForm] Product model)
        {
            bool result = false;
            var product = await repo.GetByIdAsync<Product>(model.Id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.ImageUrl = model.ImageUrl;
                product.ManufacturerId = model.ManufacturerId;
                product.Manufacturer = model.Manufacturer;
                product.Description = model.Description;
                product.UnitOfMeasureId = model.UnitOfMeasureId;
                product.UnitOfMeasure = model.UnitOfMeasure;
                product.BuyingPrice = model.BuyingPrice;
                product.SellingPrice = model.SellingPrice;
                product.CategoryId = model.CategoryId;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public void DeleteProduct(int id)
        {
            var product = repo.All<Product>()
                .Where(p => p.Id == id)
                .First();

            repo.Delete(product);

            repo.SaveChanges();
        }

        public async Task<Product?> ProductForPlaceholder(int id)
        {
            return await repo.GetByIdAsync<Product>(id);
        }

        public async Task BuyProduct(int id)
        {
            var product = await repo.GetByIdAsync<Product>(id);
            var price = product.SellingPrice;

            var cashDesk = repo.All<CashDesk>().First();
            cashDesk.TotalSum += price;

            await repo.SaveChangesAsync();
        }

        public async Task SellProduct([FromForm] SoldProduct soldProduct, int id)
        {
            var product = await repo.GetByIdAsync<Product>(id);

            if (product.Amount - soldProduct.Amount < 0)
            {
                product.Amount = 0;
            }
            else
            {
            product.Amount -= soldProduct.Amount;
            }

            if (product.Amount <= 0)
            {
                product.IsSoldOut = true;
            }

            await repo.AddAsync(new SoldProduct
            {
                Amount = soldProduct.Amount,
                Date = DateTime.Now,
                ProductId = id,
                Product = product
            });

            var price = product.SellingPrice;

            var cashDesk = repo.All<CashDesk>().First();
            cashDesk.TotalSum += price*(decimal)product.Amount;

            await repo.SaveChangesAsync();
        }

        public async Task RestockProduct([FromForm] RestockViewModel restockProduct)
        {
            var product = await repo.GetByIdAsync<Product>(restockProduct.ProductId);

            if (product != null)
            {
                product.Amount += restockProduct.Amount;
                product.IsSoldOut = false;
            }

            await repo.AddAsync(new RestockProduct
            {
                Amount=restockProduct.Amount,
                Date=DateTime.Now,
                ProductId = product.Id,
                Product = product
            });

            var price = product.BuyingPrice;

            var cashDesk = repo.All<CashDesk>().First();
            cashDesk.TotalSum -= price * (decimal)product.Amount;

            await repo.SaveChangesAsync();
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return repo.All<Manufacturer>().ToList();
        }

        public IEnumerable<UnitOfMeasure> GetUnitOfMeasures()
        {
            return repo.All<UnitOfMeasure>().ToList();
        }

        public CashDesk GetCashDesk()
        {
            return repo.All<CashDesk>().First();
        }
    }

}
