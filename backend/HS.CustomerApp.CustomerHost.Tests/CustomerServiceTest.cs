using System.Threading.Tasks;
using FluentAssertions;
using HS.CustomerApp.CustomerHost.Logic;
using HS.CustomerApp.CustomerHost.Models;
using HS.CustomerApp.IdClient;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Snapshooter.Xunit;
using Xunit;

namespace HS.CustomerApp.CustomerHost.Tests
{
    public class CustomerServiceTest
    {
        [Fact]
        public void ShouldReturnAllCustomers()
        {
            // Arrange
            var sut = new CustomerService(GetIdClientMock().Object, GetNullLogger());

            // Act
            var result = sut.ReadAll();

            // Assert
            Snapshot.Match(result);
        }

        [Theory]
        [InlineData(1, "Microsoft")]
        [InlineData(2, "Oracle")]
        [InlineData(3, "IBM")]
        public void ShouldReturnSingleCustomer(long id, string expectedName)
        {
            // Arrange
            var sut = new CustomerService(GetIdClientMock().Object, GetNullLogger());

            // Act
            var result = sut.Read(id);

            // Assert
            result.Id.Should().Be(id);
            result.Name.Should().Be(expectedName);
        }

        [Fact]
        public void ShouldDeleteCustomer()
        {
            // Arrange
            var sut = new CustomerService(GetIdClientMock().Object, GetNullLogger());

            // Act
            sut.Delete(1);

            // Assert
            var item = sut.Read(1);
            item.Should().BeNull();
        }

        [Fact]
        public async Task ShouldAddCustomer()
        {
            // Arrange
            var sut = new CustomerService(GetIdClientMock().Object, GetNullLogger());
            var customer = new CustomerModel
            {
                Name = "Facebook",
                Location = "USA"
            };

            // Act
            var id = await sut.Create(customer);

            // Assert
            var item = sut.Read(id);
            item.Id.Should().Be(id);
            item.Name.Should().Be(customer.Name);
            item.Location.Should().Be(customer.Location);
        }

        [Fact]
        public async Task ShouldUpdateCustomer()
        {
            // Arrange
            var sut = new CustomerService(GetIdClientMock().Object, GetNullLogger());
            var customer = new CustomerModel
            {
                Name = "Facebook",
                Location = "USA"
            };
            var id = await sut.Create(customer);

            // Act
            var updatedCustomer = new CustomerModel
            {
                Id = id,
                Name = "Facebook Inc",
                Location = "Menlo Park, California, USA"
            };
            sut.Update(updatedCustomer);

            // Assert
            var item = sut.Read(id);
            item.Should().Be(updatedCustomer);
        }

        private static Mock<IIdClient> GetIdClientMock()
        {
            var idClientMock = new Mock<IIdClient>();
            idClientMock.Setup(x => x.GenerateAsync()).ReturnsAsync(4711);
            return idClientMock;
        }

        private static NullLogger<CustomerService> GetNullLogger() => new NullLogger<CustomerService>();
    }
}