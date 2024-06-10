using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Core.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Response<IEnumerable<Product>>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<IEnumerable<Product>>>
    {
        private readonly IProductRepositoryAsync _productRepository;

        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<IEnumerable<Product>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return new Response<IEnumerable<Product>>(products);
        }
    }
}

