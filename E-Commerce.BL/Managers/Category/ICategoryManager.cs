using E_Commerce.BL.Dtos;

namespace E_Commerce.BL.Managers.Category;

public interface ICategoryManager
{
    void AddCategory(CategoryDto categoryDto);
    IEnumerable<CategoryDto> GetAllCategories();
}
