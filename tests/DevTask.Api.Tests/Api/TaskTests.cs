using System.Net.Http.Json;
using DevTask.Core.Models.Messages;
using Moq;

namespace DevTask.Api.Tests.Api
{
    public class TaskTests : IClassFixture<ApiFactory>
    {
        private readonly ApiFactory _factory;

        public TaskTests(ApiFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Create_Complete_Should_Publish_Event()
        {
            var client = _factory.CreateClient();

            var createResponse = await client.PostAsJsonAsync("/tasks", new
            {
                title = "Test",
                isCompleted = false,
                priority = "High"
            }, CancellationToken.None);

            createResponse.EnsureSuccessStatusCode();

            var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>(CancellationToken.None);
            
            var completeResponse = await client.PutAsync($"/tasks/{createdId}/complete", null, CancellationToken.None);
            completeResponse.EnsureSuccessStatusCode();

            _factory.RabbitMock.Verify(
                x => x.PublishAsync(It.IsAny<IMetadataDefinition>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
