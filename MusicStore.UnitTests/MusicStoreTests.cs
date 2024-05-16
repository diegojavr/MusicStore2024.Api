using AutoMapper;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MusicStore.Domain;
using MusicStore.Domain.Info;
using MusicStore.Dto.Request;
using MusicStore.Persistence;
using MusicStore.Repositories.Implementations;
using MusicStore.Repositories.Interfaces;
using MusicStore.Services.Implementations;
using MusicStore.Services.Interfaces;
using MusicStore.Services.Profiles;
using System.Diagnostics;

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

            var list = new List<Concert>();
            for (int i = 0; i< 100; i++)
            {
                list.Add(new Concert() { Title = $"Concert No. {i}" });
            }

            var selector =list
                .Select(x=> new ConcertInfo())
                .ToList();

            repository
                .Setup(p => p.ListAsync("", 1, 10))
                .ReturnsAsync((selector, list.Count));


            var logger = new Mock<ILogger<ConcertService>>();
            var mapper = new Mock<IMapper>();
            var fileUploader = new Mock<IFileUploader>();

            IConcertService service = new ConcertService(repository.Object, logger.Object, mapper.Object, fileUploader.Object);

            //Act
            var response = await service.ListAsync("", 1, 10);
            var expected = 10;
            //Assert

            Debug.WriteLine($"Cantidad de objetos devueltos {response.TotalPages}");
            Assert.Equal(expected, response.TotalPages);
        }

        [Theory]
        [InlineData(30,true)]
        [InlineData(-30, false)]
        public async Task SaleCheckTest(int days, bool expectedSuccess)
        {
            //Arrange
            var concert = new Concert()
            {
                Id = 1,
                Title = "Concierto de prueba de rock",
                DateEvent = DateTime.Now.AddDays(days),
                GenreId = 1,
                ImageUrl = "https://google.com/images",
                UnitPrice = 100,
                TicketsQuantity = 100,
            };

            var anyCustomer = It.IsAny<string>();

            var concertRepository = new Mock<IConcertRepository>();

            concertRepository.Setup(p=>p.FindByIdAsync(1))
                .ReturnsAsync(concert);

            var saleRepository = new Mock<ISaleRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

            customerRepository.Setup(p => p.FindByEmailAsync(anyCustomer))
                .ReturnsAsync(new Customer
                {
                    Id = 1,
                    Email = anyCustomer
                });

            var logger = new Mock<ILogger<SaleService>>();

            //Se agrega mapeo con Automapper ya que es necesaria para la depuración del perfil de venta
            var mapper = new Mapper(new MapperConfiguration(p=> p.AddProfile<SaleProfile>()));

            var service = new SaleService(saleRepository.Object, 
                logger.Object, 
                mapper,
                concertRepository.Object,
                customerRepository.Object);

            //Act
            var request = new SaleDtoRequest()
            {
                ConcertId = 1,
                TicketsQuantity = 10
            };
            var response = await service.AddAsync(anyCustomer,request);
            


            //Assert
            Debug.WriteLine(response.ErrorMessage);
            Assert.Equal(expectedSuccess, response.Success);

        }
    }
}