using AutoMapper;
using CleanArchitecture.Core.DTOs.Product;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category
            };

            await _productRepository.AddAsync(product);

            return new Response<int>(product.Id);
        }

        public async Task<Response<int>> DeleteProductAsync(DeleteProductRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new ApiException($"Product with Id {request.Id} not found.");
            }

            await _productRepository.DeleteAsync(product);

            return new Response<int>(product.Id);
        }

        public async Task<Response<ProductResponse>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new ApiException($"Product with Id {id} not found.");
            }

            var productResponse = _mapper.Map<ProductResponse>(product);

            return new Response<ProductResponse>(productResponse);
        }

        public async Task<Response<IEnumerable<ProductResponse>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return new Response<IEnumerable<ProductResponse>>(productResponses);
        }
    }
}

