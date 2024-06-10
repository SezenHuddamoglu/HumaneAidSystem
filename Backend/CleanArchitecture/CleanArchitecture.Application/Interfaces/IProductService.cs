using CleanArchitecture.Core.DTOs.Product;
using CleanArchitecture.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IProductService
    {
        Task<Response<int>> CreateProductAsync(CreateProductRequest request);
        Task<Response<int>> DeleteProductAsync(DeleteProductRequest request);
        Task<Response<ProductResponse>> GetProductByIdAsync(int id);
        Task<Response<IEnumerable<ProductResponse>>> GetAllProductsAsync();
    }
}


