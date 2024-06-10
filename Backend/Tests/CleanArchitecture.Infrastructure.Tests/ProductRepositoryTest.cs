using AutoFixture;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Infrastructure.Tests
{
    public class ProductRepositoryTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IDateTimeService> _dateTimeService;
        private readonly Mock<IAuthenticatedUserService> _authenticatedUserService;
        private readonly Product existingProduct;
        private readonly ApplicationDbContext context;


        public ProductRepositoryTest() {

            this._fixture = new Fixture();
            this.existingProduct = _fixture.Create<Product>();
            _dateTimeService = new Mock<IDateTimeService>();
            _authenticatedUserService = new Mock<IAuthenticatedUserService>();  

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(_fixture.Create<string>());

            context = new ApplicationDbContext(optionsBuilder.Options, _dateTimeService.Object, _authenticatedUserService.Object);

            context.Products.Add(existingProduct);
            context.SaveChanges();
        }


        [Fact]
        public void When_IsUniqueBarcodeAsyncCalledWithExistingBarcode_ShouldReturnFalse()
        {
            var repository = new ProductRepositoryAsync(context);

            var result = repository.IsUniqueBarcodeAsync(existingProduct.Barcode).Result;
            Assert.False(result);
        }


        [Fact]
        public void When_IsUniqueBarcodeAsyncCalledWithNotExistingBarcode_ShouldReturnTrue()
        {
            var repository = new ProductRepositoryAsync(context);

            var result = repository.IsUniqueBarcodeAsync(_fixture.Create<string>()).Result;
            Assert.True(result);
        }
    }
}