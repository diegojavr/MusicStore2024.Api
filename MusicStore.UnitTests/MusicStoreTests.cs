using AutoMapper;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MusicStore.Domain;
using MusicStore.Persistence;
using MusicStore.Repositories.Implementations;
using MusicStore.Repositories.Interfaces;
using MusicStore.Services.Implementations;
using MusicStore.Services.Interfaces;

namespace MusicStore.UnitTests
{
    public class MusicStoreTests
    {

        private static async Task<MusicStoreDbContext> ArrangeDatabase()
        {
            //Se crea DB in memory para poder utilizarlo dentro del dbContext
            var options = new DbContextOptionsBuilder<MusicStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new MusicStoreDbContext(options);
            await dbContext.Database.EnsureCreatedAsync(); //ejecuta las migraciones
            return dbContext;
        }
        [Fact]
        public async void AgregarGeneroTest()
        {
            //Arrange

            var dbContext = await ArrangeDatabase();

            //Act

            var genre = new Genre()
            {
                Name = "Rock test",
            };

            await dbContext.Set<Genre>().AddAsync(genre);
            await dbContext.SaveChangesAsync();

            var idExpected = 1;

            //Assert

            Assert.Equal(idExpected, genre.Id);
        }

        [Fact]
        public async Task GenreRepositoryTest()
        {
            //Arrange

            var dbContext = await ArrangeDatabase();
            var genreRepository = new GenreRepository(dbContext);

            //Act
            var result = await genreRepository.AddAsync(new Genre()
            {
                Name = "Rock test  2"
            });

            var expected = 1;
            var generoCreado = await genreRepository.FindByIdAsync(expected);

            //Assert
            Assert.Equal(expected, result);

            Assert.NotNull(generoCreado);
        }

        [Fact]
        public async Task CalculoPaginacionConcertTest()
        {
            //Arrange
            
            var repository = new Mock<IConcertRepository>();
            var logger = new Mock<ILogger<ConcertService>>();
            var mapper = new Mock<IMapper>();
            var fileUploader = new Mock<IFileUploader>();

            IConcertService service = new ConcertService(repository.Object, logger.Object, mapper.Object, fileUploader.Object);

            //Act
            var response = await service.ListAsync("", 1, 10);
            var expected = 0;
            //Assert

            Assert.Equal(expected, response.TotalPages);
        }
    }
}