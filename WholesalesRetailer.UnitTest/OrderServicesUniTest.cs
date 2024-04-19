using Microsoft.Extensions.Logging;
using Moq;
using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Services;
using WholesalesRetailer.Shared.Models;
using WholesalesRetailer.UnitTest.Mocks;

namespace WholesalesRetailer.UnitTest
{
    public class OrderServicesUniTest
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ILogger<OrderService>> _loggerMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public OrderServicesUniTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _loggerMock = new Mock<ILogger<OrderService>>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task GetOrders_WhenRepositoryReturnsData_ReturnsListOfOrders()
        {
            //Arange
            var list = OrderFEMocks.GetOrders();
            _orderRepositoryMock.Setup(repo => repo.GetOrders()).Returns(list);

            var orderService = new OrderService(_loggerMock.Object, _orderRepositoryMock.Object, _productRepositoryMock.Object, _customerRepositoryMock.Object);

            // Act
            var result = orderService.GetOrders();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void GetOrders_WhenRepositoryThrowsException_ReturnsNull()
        {
            // Arrange

            _orderRepositoryMock.Setup(repo => repo.GetOrders()).Throws(new Exception("Test exception"));
            var _orderService = new OrderService(_loggerMock.Object, _orderRepositoryMock.Object, _productRepositoryMock.Object, _customerRepositoryMock.Object);

            // Act
            var result = _orderService.GetOrders();

            // Assert
            Assert.Null(result);
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(),
                                It.IsAny<EventId>(), 
                                It.Is<It.IsAnyType>((v, t) => true),
                                It.IsAny<Exception>(),
                                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }

        [Fact]
        public void AddNewOrder_ReturnsExpectedOrder()
        {
            // Arrange
            _orderRepositoryMock.Setup(repo => repo.GetLastOrderId()).Returns(10);

            _productRepositoryMock.Setup(repo => repo.GetProductIdByCode(It.IsAny<string>())).Returns(1);
            _productRepositoryMock.Setup(repo => repo.GetUnitPriceByCode(It.IsAny<string>())).Returns(10);
            _productRepositoryMock.Setup(repo => repo.GetQuantity(It.IsAny<string>())).Returns(20);
            _orderRepositoryMock.Setup(repo => repo.GetRebate(It.IsAny<int>())).Returns(5);
            _orderRepositoryMock.Setup(repo => repo.GetCashBack(It.IsAny<int>())).Returns(200);

            _customerRepositoryMock.Setup(repo => repo.GetCustomerName(It.IsAny<int>())).Returns("Test Customer");

            var loggerMock = new Mock<ILogger<OrderService>>();
            var orderService = new OrderService(_loggerMock.Object, _orderRepositoryMock.Object, _productRepositoryMock.Object, _customerRepositoryMock.Object);

            var receiveOrder = new ReceiveOrder
            {
                CustomerId = 1,
                ProductData = new List<ProductData>
                {
                    new ProductData { ProductCode = "PC1", Quantity = 5 },
                    new ProductData { ProductCode = "PC2", Quantity = 3 }
                }
            };

            // Act
            var result = orderService.AddNewOrder(receiveOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(11, result.OrderId);
            Assert.Equal(1, result.CustomerId);
            Assert.Equal("Test Customer", result.CustomerName);

            // Verify repository interactions
            _orderRepositoryMock.Verify(repo => repo.InsertOrderItems(It.IsAny<OrderItems>()), Times.Exactly(2));
            _orderRepositoryMock.Verify(repo => repo.InsertOrder(It.IsAny<OrderFE>()), Times.Once);
        }
    }
}
