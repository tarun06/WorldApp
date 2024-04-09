using Microsoft.EntityFrameworkCore;
using Moq;
using WorldApp.WPF.Database;
using WorldApp.WPF.Model;
using WorldApp.WPF.Repository;

namespace A2TarunKalal.Test
{
    [TestClass]
    public class CountryRepositoryTest
    {
        private Mock<DbSet<Country>> _mockSet;
        private Mock<WorldDBContext> _worldDBContext;

        public CountryRepositoryTest()
        {
            _worldDBContext = new Mock<WorldDBContext>() { DefaultValue = DefaultValue.Mock };

            var data = new List<Country>
            {
               new Country()
               {
                   CountryId = 1, CountryName = "Canada", Currency = "Canadian dollar", Language = "English",
                   Continent= new Continent(){ ContinentId = 1, ContinentName = "North Amertica"}
               }
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Country>>();
            _mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            _worldDBContext.Setup(c => c.Country).Returns(_mockSet.Object);
        }

        [TestMethod]
        public async Task AddCountryAsync_ThrowArgumentNullException_WhenContinent_Exist()
        {
            var repo = new CountryRepository(_worldDBContext.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await repo.AddCountryAsync(new Country() { CountryName = "Aruba" });
            });
        }

        [TestMethod]
        public async Task AddCountryAsync_ThrowArgumentNullException_WhenContinent_IsEmpty()
        {
            var repo = new CountryRepository(_worldDBContext.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await repo.AddCountryAsync(new Country());
            });
        }

        [TestMethod]
        public async Task AddCountryAsyncAsync_Execute_Success()
        {
            var repo = new CountryRepository(_worldDBContext.Object);

            await repo.AddCountryAsync(new Country()
            {
                Continent = new Continent() { ContinentId = 1, ContinentName = "North Amertica" },
                CountryName = "Aruba",
                Language = "English",
                Currency = "Dollar"
            });

            _mockSet.Verify(m => m.Add(It.IsAny<Country>()), Times.Once());

            _worldDBContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }
    }
}