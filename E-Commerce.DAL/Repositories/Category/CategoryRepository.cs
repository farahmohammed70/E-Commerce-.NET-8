using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.Category;

public class CategoryRepository : GenericRepository<Data.Models.Category>, ICategoryRepository
{
    public CategoryRepository(MyAppContext context) : base(context)
    {
    }

    public Data.Models.Category? GetByName(string name)
    {
        return _dbContext.Categories.FirstOrDefault(c => c.Name == name);
    }

}
