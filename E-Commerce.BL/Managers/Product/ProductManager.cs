using E_Commerce.BL.Dtos;
using E_Commerce.DAL.Data;
using Microsoft.AspNetCore.Http;


namespace E_Commerce.BL.Managers.Product
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProductDetailsDto GetById(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with id {id} not found");
            }

            var category = _unitOfWork.CategoryRepository.GetById(product.CategoryId);
            var categoryName = category != null ? category.Name : "Unknown";

            return new ProductDetailsDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageFile = product.PictureUrl,
                CategoryName = categoryName,
            };
        }

        public void AddProduct(ProductDto productDto)
        {
            var category = _unitOfWork.CategoryRepository.GetByName(productDto.CategoryName);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            var productEntity = new DAL.Data.Models.Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                PictureUrl = UploadImage(productDto.ImageFile!),
                CategoryId = category.Id,
            };

            _unitOfWork.ProductRepository.Add(productEntity);
            _unitOfWork.SaveChanges();

        }


        public void DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with id {id} not found");
            }

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChanges();

        }

        public IEnumerable<ViewProductDto> GetProducts(string? categoryName, string? productName)
        {
            var products = _unitOfWork.ProductRepository.GetProducts(categoryName, productName)
                       .Select(p => new ViewProductDto
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Image = p.PictureUrl!,
                           CategoryName = _unitOfWork.CategoryRepository.GetById(p.CategoryId)?.Name ?? "Unknown",
                           Price = p.Price
                       });

            return products;
        }

        public void UpdateProduct(int id, ProductDto productDto)
        {
            var existingProduct = _unitOfWork.ProductRepository.GetById(id);
            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with id {id} not found");
            }
            var category = _unitOfWork.CategoryRepository.GetByName(productDto.CategoryName);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            existingProduct.Description = productDto.Description;
            existingProduct.PictureUrl = UploadImage(productDto.ImageFile!);
            existingProduct.CategoryId = category.Id;

            _unitOfWork.ProductRepository.Update(existingProduct);
            _unitOfWork.SaveChanges();

        }

        private string UploadImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return string.Empty;
            }
            string[] allowedExtensions = new string[] { ".jpg", ".svg", ".png" };
            var fileExtensions = Path.GetExtension(imageFile.FileName);
            if (!allowedExtensions.Contains(fileExtensions))
            {
                throw new ArgumentException("Invalid file format. Allowed formats are: .jpg, .svg, .png");
            }

            var fileName = Guid.NewGuid().ToString() + fileExtensions;

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return fileName;
        }
    }
}
