using E_magazin.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_magazin.Contracts
{
    public interface ICategoryServices
    {
        Task AddCategory([FromForm] Category category);

        IEnumerable<Category> GetCategories();

        Task<bool> EditCategory([FromForm] Category model);

        bool DeleteCategory(int id);

    }
}
