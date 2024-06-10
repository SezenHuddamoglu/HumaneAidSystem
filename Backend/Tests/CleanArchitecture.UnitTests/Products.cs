using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Core.Interfaces.Repositories;
using Moq;
using static CleanArchitecture.Core.Features.Products.Commands.UpdateProduct.UpdateProductCommand;

namespace CleanArchitecture.UnitTests
{
    public class Products
    {
        private readonly Fixture fixture;
        private readonly Mock<IProductRepositoryAsync> productRepositoryAsync;

        public Products()
        {
            fixture = new Fixture();
            productRepositoryAsync = new Mock<IProductRepositoryAsync>();
        }                   


        [Fact]
        public void When_UpdateProductCommandHandlerInvoked_WithNotExistingProduct_ShouldThrowEntityNotFoundException()
        {
            productRepositoryAsync
                .Setup(pr => pr.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var updateProductCommandHandler = new UpdateProductCommandHandler(productRepositoryAsync.Object);

            var command = this.fixture.Create<UpdateProductCommand>();
            var cancellationToken = this.fixture.Create<CancellationToken>();

            Assert.ThrowsAsync<EntityNotFoundException>(()=>updateProductCommandHandler.Handle(command, cancellationToken));
        }

        [Fact]
        public void When_UpdateProductCommandHandlerInvoked_WithNotUniqueBarcode_ShouldThrowBarcodeIsNotUniqueException()
        {
            this.productRepositoryAsync
                .Setup(pr=> pr.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(this.fixture.Create<Product>());

            this.productRepositoryAsync
                .Setup(pr => pr.IsUniqueBarcodeAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var updateProductCommandHandler = new UpdateProductCommandHandler(this.productRepositoryAsync.Object);

            var command = this.fixture.Create<UpdateProductCommand>();
            var cancellationToken = this.fixture.Create<CancellationToken>();

            Assert.ThrowsAsync<BarcodeIsNotUniqueException>(() => updateProductCommandHandler.Handle(command, cancellationToken));
        }

        [Fact]
        public async Task When_UpdateProductCommandHandlerInvoked_ShouldReturnProductId()
        {
            Product product = this.fixture.Create<Product>();
            this.fixture.Customize<UpdateProductCommand>(c => c.With(x => x.Id, product.Id));

            this.productRepositoryAsync
              .Setup(pr => pr.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(product);

            this.productRepositoryAsync
                .Setup(pr => pr.IsUniqueBarcodeAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var updateProductCommandHandler = new UpdateProductCommandHandler(this.productRepositoryAsync.Object);

            var command = this.fixture.Create<UpdateProductCommand>();
            
            var cancellationToken = this.fixture.Create<CancellationToken>();

            var result = await updateProductCommandHandler.Handle(command, cancellationToken);
            
            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Data);
           
        }
    }
}