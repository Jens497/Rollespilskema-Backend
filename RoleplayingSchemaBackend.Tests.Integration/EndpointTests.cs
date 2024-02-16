using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Testing;
using RoleplayingSchemaBackend.Data;
using System.Net;
using System.Net.Http.Json;

namespace RoleplayingSchemaBackend.Tests.Integration
{
    public class EndpointTests : IClassFixture<WebApplicationFactory<IApiMarker>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<IApiMarker> _webApplicationFactory;
        private HttpClient _httpClient;

        public EndpointTests(WebApplicationFactory<IApiMarker> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _httpClient = _webApplicationFactory.CreateClient();
        }

        private async Task AdminLogin() =>
            await _httpClient.PostAsJsonAsync("api/auth/Login", new UserLogin { UserName = "AdminTest", Password = "stringD123" });

        private async Task UserLogin() => 
            await _httpClient.PostAsJsonAsync("api/auth/Login", new UserLogin { UserName = "UserTest", Password = "stringD123" });

        private async Task Logout() => await _httpClient.PostAsync("api/auth/Logout", null);

        [Fact]
        public async Task TestUnauthorized()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            //Act
            var response = await httpClient.GetAsync("api/user/");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task TestLogout()
        {
            //Arrange
            await UserLogin();
            //Act
            var resp = await _httpClient.GetAsync("api/user/me");
            //Assert
            resp.StatusCode.Should().Be(HttpStatusCode.OK);

            //Arrange
            await Logout();
            //Act
            var res = await _httpClient.GetAsync("api/user");
            //Assert
            res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task TestRoleAccess()
        {
            await AdminLogin();
            var resp = await _httpClient.GetAsync("api/user");
            resp.StatusCode.Should().Be(HttpStatusCode.OK);

            await Logout();

            await UserLogin();
            var res = await _httpClient.GetAsync("api/user");
            res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateTemplate()
        {
            await AdminLogin();
            Guid templateGuid = new Guid();
            Guid componentId = new Guid();
            Component comp = new Component()
            {
                ComponentId = componentId,
                Name = "NewComp",
                Properties = "test properties"
            };
            IEnumerable<Component> compList = new List<Component> {comp};
            var resp = await _httpClient.PostAsJsonAsync("api/template", new Template { TemplateId = templateGuid, Name = "TestTemplate", Components = compList });
            resp.StatusCode.Should().Be(HttpStatusCode.OK);

            var templates = await _httpClient.GetAsync(string.Format("api/template/templates?ids={0}", templateGuid));
            var respString = templates.ToString();
            Assert.True(respString.Any());

            //Clean up
            await _httpClient.DeleteAsync(string.Format("api/template?templateId={0}", templateGuid));
        }
        //Logging out in both the tests, to make sure nothing is logged in.
        public async Task InitializeAsync()
        {
            Console.WriteLine("Starting test");
            await Logout();
        }

        public async Task DisposeAsync()
        {
            Console.WriteLine("Starting teardown of test");
            await Logout();
        }
    }
}