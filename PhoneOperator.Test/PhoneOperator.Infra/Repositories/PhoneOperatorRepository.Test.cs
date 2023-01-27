using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PhoneOperator.Infra.Context;
using PhoneOperator.Domain.Models;
using PhoneOperator.Infra.Repositories;
using PhoneOperator.Domain.Interfaces;

namespace PhoneOperator.Test.Infra.Repositories
{
    public class PhoneOperatorRepositoryTest
    {
        [Fact]
        public void Should_Return_Success_GetAllPhoneOperators()
        {
            // Arrange
            var mockContextOptions = new DbContextOptionsBuilder<DatabaseContext>();
            var mockContext = new Mock<DatabaseContext>(mockContextOptions.Options);
            var mockSet = new Mock<DbSet<Operator>>();
            var data = new List<Operator> { new Operator {
                Id = 12321,
                Name = "ASfasdfasdf",
                OperatorCode = 312
             } }.AsQueryable();

            mockSet.As<IQueryable<Operator>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Operator>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Operator>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Operator>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(m => m.Operators).Returns(mockSet.Object);

            // Act
            var repository = new PhoneOperatorRepository(mockContext.Object);
            // repository.Setup(m => m.GetAllOperators()).Returns(data.ToList());
            var phoneOperators = repository.GetAllOperators();

            // Assert
            Assert.NotNull(phoneOperators);
        }

        [Theory]
        [InlineData(21)]
        public void Should_Return_Success_GetPhoneOperatorById(int id)
        {
            // Arrange
            var data = new Operator()
            {
                Id = 12321,
                Name = "ASfasdfasdf",
                OperatorCode = 312
            };

            // Act
            var repository = new Mock<IPhoneOperatorRepository>();
            repository.Setup(r => r.GetOperatorById(id)).Returns(data);
            var phoneOperators = repository.Object.GetOperatorById(id);

            // Assert
            Assert.NotNull(phoneOperators);
        }
    }
}