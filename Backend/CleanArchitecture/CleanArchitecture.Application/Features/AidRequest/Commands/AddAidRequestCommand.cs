using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreProduct = CleanArchitecture.Core.Entities.Product;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidRequest.Commands
{
    public class AddAidRequestCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public List<int> ProductIds { get; set; }
        public string AidPointId { get; set; }
    }

    public class AddAidRequestCommandHandler : IRequestHandler<AddAidRequestCommand, Response<int>>
    {
        private readonly IAidRequestRepositoryAsync _aidRequestRepository;
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IAidPointRepositoryAsync _aidPointRepository;

        public AddAidRequestCommandHandler(
            IAidRequestRepositoryAsync aidRequestRepository,
            IProductRepositoryAsync productRepository,
            IAidPointRepositoryAsync aidPointRepository)
        {
            _aidRequestRepository = aidRequestRepository;
            _productRepository = productRepository;
            _aidPointRepository = aidPointRepository;
        }

        public async Task<Response<int>> Handle(AddAidRequestCommand request, CancellationToken cancellationToken)
        {
            // Aid point kontrolü
            var aidPoint = await _aidPointRepository.GetByIdAsync(request.AidPointId);
            if (aidPoint == null)
            {
                throw new ApiException($"Aid point with Id {request.AidPointId} not found.");
            }

            // Seçilen ürünlerin kontrolü
            var selectedProducts = new List<CoreProduct>();

            foreach (var productId in request.ProductIds)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    throw new ApiException($"Product with Id {productId} not found.");
                }
                selectedProducts.Add(product);
            }

            var aidRequest = new CleanArchitecture.Application.Entities.AidRequest
            {
                //Name = request.Name,
                //Description = request.Description,
                //Location = request.Location,
                Products = selectedProducts,
                AidPointId = request.AidPointId
            };

            await _aidRequestRepository.AddAsync(aidRequest);

            return new Response<int>(aidRequest.Id);
        }
    }
}
