using E_magazin.Contracts;
using E_magazin.Data.Models;
using E_magazin.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Services
{
    public class CategoryServices : ICategoryServices
    {
        private IApplicationDbRepository repo;

        public CategoryServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddCategory([FromForm] Category category)
        {
            var existing = repo.All<Category>()
               .Where(r => r.Name == category.Name)
               .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("Category already exist");
            }

            await repo.AddAsync(new Category()
            {
                Name = category.Name,
            });

            await repo.SaveChangesAsync();
        }    

        public async Task<bool> EditCategory([FromForm] Category model)
        {
            bool result = false;
            var category = await repo.GetByIdAsync<Category>(model.Id);

            if (category != null)
            {
                category.Name = model.Name;

                await repo.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public bool DeleteCategory(int id)
        {
            bool result = false;

            var category = repo.All<Category>()
               .Where(p => p.Id == id)
               .First();

            var products = repo.All<Product>()
                .Where(p => p.Category.Name == category.Name)
                .ToList();

            if (products.Count==0)
            {
                repo.Delete(category);

                repo.SaveChanges();

                result = true;
            }

            return result;                    
        }

        public IEnumerable<Category> GetCategories()
        {
            return repo.All<Category>();
        }
    }
}
