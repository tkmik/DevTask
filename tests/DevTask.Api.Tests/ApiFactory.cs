using DevTask.Core.Models.Messages;
using DevTask.Core.Services.Interfaces;
using DevTask.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace DevTask.Api.Tests
{
    public class ApiFactory : WebApplicationFactory<Program>
    {
        public Mock<IRabbitMqService> RabbitMock { get; } = new();
        public Mock<IRabbitMqConnection> RabbitConnectionMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            RabbitMock
               .Setup(x => x.PublishAsync(It.IsAny<IMetadataDefinition>(), It.IsAny<CancellationToken>()))
               .Returns(Task.CompletedTask);

            RabbitConnectionMock
                .Setup(x => x.GetChannelAsync(It.IsAny<CancellationToken>()))
                .Throws(new Exception("RabbitMQ disabled in tests"));

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IRabbitMqService>();
                services.RemoveAll<IRabbitMqConnection>();

                services.AddSingleton(RabbitMock.Object);
                services.AddSingleton(RabbitConnectionMock.Object);

                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.RemoveAll(typeof(DbContextOptions));

                var inMemoryOptions = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("in-memory-db")
                    .Options;

                services.AddSingleton<DbContextOptions<AppDbContext>>(inMemoryOptions);
                services.AddSingleton<DbContextOptions>(inMemoryOptions);
            });
        }
    }
}
