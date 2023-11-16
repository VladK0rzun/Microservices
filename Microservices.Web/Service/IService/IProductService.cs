using Services.Web.Models;

namespace Services.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> GetProductByIdAsync(int id);
        Task<ResponseDTO?> CreateProductsAsync(ProductDTO productDTO);
        Task<ResponseDTO?> UpdateProductsAsync(ProductDTO productDTO);
        Task<ResponseDTO?> DeleteProductsAsync(int id);
    }
}
