using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities;
using HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using CleanArchitecture.Core.Interfaces;
using CoreProduct = CleanArchitecture.Core.Entities.Product;



namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Features.AidOffer.Commands
{
    public class MakeDonationCommand : IRequest<Response<int>>
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CoreProduct> Products { get; set; }
    }

    public class ProductRequest
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; } // Amount for each product
    }

    public class MakeDonationCommandHandler : IRequestHandler<MakeDonationCommand, Response<int>>
    {
        private readonly IAidOfferRepositoryAsync _donationRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public MakeDonationCommandHandler(IAidOfferRepositoryAsync donationRepository, IUserRepositoryAsync userRepository, IAuthenticatedUserService authenticatedUserService)
        {
            _donationRepository = donationRepository;
            _userRepository = userRepository;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<int>> Handle(MakeDonationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), request.UserId);
            }

            var userRole = _authenticatedUserService.Role;
            if (userRole != "Helper")
            {
                // Add logging
                Console.WriteLine($"Unauthorized access attempt by user with role: {userRole}");
                throw new UnauthorizedAccessException("Only users with the Helper role can make donations.");
            }



            var donation = new CleanArchitecture.Application.Entities.AidOffer
            {
                UserId = request.UserId,
                //Amount = request.TotalAmount,
                Products = new List<CoreProduct>()
            };

            foreach (var productRequest in request.Products)
            {
                donation.Products.Add(new CoreProduct
                {
                    Name = productRequest.Name,
                    Category = productRequest.Category,
                    Amount = productRequest.Amount
                });
            }

            await _donationRepository.AddAsync(donation);

            return new Response<int>(donation.Id);
        }
    }
}
