using Contracts;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class EndeavourRepositoryTest
    {
        [Fact]
        public void GetAllEndeavoursAsync_ReturnListOfEndeavours_WithSingleEndeavour()
        {
            //Arrange
            var mockRepo = new Mock<IEndeavourRepository>();
            mockRepo.Setup(repo => (repo.GetAllEndeavoursAsync(false)))
                .Returns(Task.FromResult(GetEndeavours()));
       
            // Act 
            var result = mockRepo.Object.GetAllEndeavoursAsync(false)
                .GetAwaiter()
                .GetResult()
                .ToList();

            //Assert
            Assert.IsType<List<Endeavour>>(result);
            Assert.Single(result);

        }
        public IEnumerable<Endeavour> GetEndeavours()
        {
            return new List<Endeavour>
          {
             new Endeavour
             {
                Id = Guid.NewGuid(),
                Name = "Test Endeavour Name",
                Description = "Test Edeavour Description",
                GoalAmount = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
             }
          };
        }
    }
}
