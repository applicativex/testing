using Accounting.Host;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Accounting.IntegrationTests
{
    public class UserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Check_User()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var request = new
            {
                Email = $"{Guid.NewGuid().ToString("N")}@gmail.com",
                FirstName = $"{Guid.NewGuid().ToString("N")}",
                LastName = $"{Guid.NewGuid().ToString("N")}",
            };
            var createUserResponse = await client.PostAsync("api/users", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

            // Assert
            createUserResponse.EnsureSuccessStatusCode(); // Status Code 200-299

            var userId = JsonConvert.DeserializeObject<CreateUserResponse>(await createUserResponse.Content.ReadAsStringAsync()).UserId;
            var getUserResponse = await client.GetAsync($"api/users/{userId}");

            getUserResponse.EnsureSuccessStatusCode();

            var user = JsonConvert.DeserializeObject<UserResponse>(await getUserResponse.Content.ReadAsStringAsync());
            Assert.Equal("application/json; charset=utf-8", createUserResponse.Content.Headers.ContentType.ToString());
            Assert.Equal(request.FirstName, user.FirstName);
            Assert.Equal(request.LastName, user.LastName);
            Assert.Equal(request.Email, user.Email);
        }
    }

    public class CreateUserResponse
    {
        public Guid UserId { get; set; }
    }

    public class UserResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
