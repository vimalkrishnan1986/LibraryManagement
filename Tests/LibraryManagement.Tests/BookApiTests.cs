using FluentAssertions;
using LibraryManagement.Api;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

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
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestMethod]
        public async Task PostBook()
        {
            var response = await _httpClient!.PostAsync("api/books", new StringContent(JsonConvert.SerializeObject(new BookDomainModel
            {
                Name = $"Name {Guid.NewGuid()}",
                AuthorName = $"AuthName{Guid.NewGuid()}"

            })));
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        [TestMethod]
        public async Task GetBooks()
        {
            var response = await _httpClient!.GetAsync("api/books");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}