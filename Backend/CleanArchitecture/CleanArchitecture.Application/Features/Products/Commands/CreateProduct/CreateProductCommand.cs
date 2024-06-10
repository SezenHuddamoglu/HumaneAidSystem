using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CoreProduct = CleanArchitecture.Core.Entities.Product;


namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly IProductRepositoryAsync _productRepository;

        public CreateProductCommandHandler(IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new CoreProduct
            {
                Name = request.Name,
                Category = request.Category
            };

            await _productRepository.AddAsync(product);

            return new Response<int>(product.Id);
        }
    }
}
