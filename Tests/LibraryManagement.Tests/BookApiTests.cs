using System.Net;
using System.Text;
using FluentAssertions;
using LibraryManagement.Api;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace LibraryManagement.Tests
{
    [TestClass]
    public class BookApiTests
    {
        private HttpClient? _httpClient;
        [TestInitialize]
        public void Initilize()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task PostBook()
        {
            await AddAsync(Guid.NewGuid()).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetBooks()
        {
            Guid resourceId = Guid.NewGuid();
            await AddAsync(resourceId).ConfigureAwait(false);
            var response = await _httpClient!.GetAsync("api/books");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetBooksById()
        {
            Guid resourceId = Guid.NewGuid();
            await AddAsync(resourceId).ConfigureAwait(false);
            var response = await _httpClient!.GetAsync($"api/books/{resourceId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task GetBooksById_failure()
        {
            var response = await _httpClient!.GetAsync($"api/books/{Guid.NewGuid}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<HttpResponseMessage> AddAsync(Guid resourceId)
        {

            var response = await _httpClient!.PostAsync("api/books", new StringContent(JsonConvert.SerializeObject(new BookDomainModel
            {
                Name = $"Name {Guid.NewGuid()}",
                AuthorName = $"AuthName{Guid.NewGuid()}",
                ResourceId = resourceId
            }), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            return response;
        }
    }
}