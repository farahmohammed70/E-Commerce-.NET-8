using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.Category;

public interface ICategoryRepository : IGenericRepository<Data.Models.Category>
{
    public Data.Models.Category? GetByName(string name);
}
