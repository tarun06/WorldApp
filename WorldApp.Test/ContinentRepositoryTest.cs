using Microsoft.EntityFrameworkCore;
using Moq;
using WorldApp.WPF.Database;
using WorldApp.WPF.Model;
using WorldApp.WPF.Repository;

namespace A2TarunKalal.Test
{
    [TestClass]
    public class ContinentRepositoryTest
    {
        private Mock<DbSet<Continent>> _mockSet;
        private Mock<WorldDBContext> _worldDBContext;

        public ContinentRepositoryTest()
        {
            _worldDBContext = new Mock<WorldDBContext>() { DefaultValue = DefaultValue.Mock };

            var data = new List<Continent>
            {
               new Continent() { ContinentId = 1, ContinentName = "Asia"}
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Continent>>();
            _mockSet.As<IQueryable<Continent>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Continent>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Continent>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Continent>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            _worldDBContext.Setup(c => c.Continent).Returns(_mockSet.Object);
        }

        [TestMethod]
        public async Task AddContinentAsync_Execute_Success()
        {
            var repo = new ContinentRepository(_worldDBContext.Object);

            await repo.AddContinentAsync("Europe");

            _mockSet.Verify(m => m.Add(It.IsAny<Continent>()), Times.Once());

            _worldDBContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }

        [TestMethod]
        public async Task AddContinentAsync_ThrowArgumentException_WhenContinent_Exist()
        {
            var repo = new ContinentRepository(_worldDBContext.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await repo.AddContinentAsync("Asia");
            });
        }

        [TestMethod]
        public async Task AddContinentAsync_ThrowArgumentNullException_WhenContinent_IsEmpty()
        {
            var repo = new ContinentRepository(_worldDBContext.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await repo.AddContinentAsync(string.Empty);
            });
        }
    }
}