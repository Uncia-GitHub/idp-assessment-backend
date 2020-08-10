using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using IdentityModel.Client;

namespace AssessmentTests
{
    [TestClass]
    public class ApiTests
    {
        private readonly HttpClient _client;

        public ApiTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task CallUnsecureApi_NoAuth_Ok()
        {
            // Arrange
            /*
             * use TestServer class to create a client and call "api/values"
             */
            var expected = HttpStatusCode.OK;

            // Act

            // uncomment the line below
            // var response = await client.GetAsync(url);

            // Assert that response.StatusCode matches expectedStatus

            var response = await _client.GetAsync("http://localhost:5000/api/values");


            response.EnsureSuccessStatusCode();
            Assert.AreEqual(expected, response.StatusCode);
        }

        [TestMethod]
        public async Task CallSecureApiRoot_WithToken_ResponseOk()
        {
            // Arrange
            /*
             * use TestServer class to create a client and call "api/securevalues"
             * get a valid access token from https://demo.identityserver.io and use it in the request
             */
            var expectedStatus = HttpStatusCode.OK;

            // Act

            // uncomment the line below
            // var response = await client.GetAsync(url);

            // Assert that response.StatusCode matches expectedStatus

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://demo.identityserver.io/.well-known/openid-configuration");


            //request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "m2m",
                ClientSecret = "secret",
                Scope = "api"
            });

            // call api
            _client.SetBearerToken(tokenResponse.AccessToken);

            var response = await _client.GetAsync("http://localhost:5000/api/securevalues");

            response.EnsureSuccessStatusCode();
            Assert.AreEqual(expectedStatus, response.StatusCode);
        }

    }
}
