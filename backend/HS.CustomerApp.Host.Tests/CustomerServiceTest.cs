using FluentAssertions;
using HS.CustomerApp.Host.Logic;
using HS.CustomerApp.Host.Models;
using Snapshooter.Xunit;
using Xunit;

namespace HS.CustomerApp.Host.Tests
{
    public class CustomerServiceTest
    {
        [Fact]
        public void ShouldReturnAllCustomers()
        {
            // Arrange
            var sut = new CustomerService();

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
            var sut = new CustomerService();

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
            var sut = new CustomerService();

            // Act
            sut.Delete(1);

            // Assert
            var item = sut.Read(1);
            item.Should().BeNull();
        }

        [Fact]
        public void ShouldAddCustomer()
        {
            // Arrange
            var sut = new CustomerService();
            var customer = new CustomerModel
            {
                Id = 4711,
                Name = "Facebook",
                Location = "USA"
            };

            // Act
            sut.Create(customer);

            // Assert
            var item = sut.Read(4711);
            item.Should().Be(customer);
        }

        [Fact]
        public void ShouldUpdateCustomer()
        {
            // Arrange
            var sut = new CustomerService();
            var customer = new CustomerModel
            {
                Id = 4711,
                Name = "Facebook",
                Location = "USA"
            };
            sut.Create(customer);

            // Act
            var updatedCustomer = new CustomerModel
            {
                Id = 4711,
                Name = "Facebook Inc",
                Location = "Menlo Park, California, USA"
            };
            sut.Update(updatedCustomer);

            // Assert
            var item = sut.Read(4711);
            item.Should().Be(updatedCustomer);
        }
    }
}