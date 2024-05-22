using E_Commerce.BL.Dtos;
using E_Commerce.DAL.Data;

namespace E_Commerce.BL.Managers.Category;

public class CategoryManager : ICategoryManager
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void AddCategory(CategoryDto addCategoryDto)
    {
        var category = _unitOfWork.CategoryRepository.GetByName(addCategoryDto.Name);
        if (category != null)
        {
            throw new ArgumentException($"Sorry!! Category {addCategoryDto.Name} is already exist");
        }
        var CategoryEntity = new DAL.Data.Models.Category
        {
            Name = addCategoryDto.Name,
            Description = addCategoryDto.Description,
        };
        _unitOfWork.CategoryRepository.Add(CategoryEntity);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<CategoryDto> GetAllCategories()
    {
        var categories = _unitOfWork.CategoryRepository.GetAll()
                  .Select(p => new CategoryDto
                  {
                      Name = p.Name,
                      Description = p.Description
                  });

        return categories;
    }
}
