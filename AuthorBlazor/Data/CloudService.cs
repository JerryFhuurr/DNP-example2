using AuthorBlazor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthorBlazor.Data
{
    public class CloudService : IAuthorBookService
    {
        private string uri = "https://localhost:5001";
        private readonly HttpClient httpClient;

        public CloudService()
        {
            httpClient = new HttpClient();
        }

        public async Task<IList<Book>> GetBooksAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(uri + "/api/Book");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

            string message = await response.Content.ReadAsStringAsync();
            List<Book> list = JsonSerializer.Deserialize<List<Book>>(message);
            return list;
        }

        public async Task<IList<Author>> GetAuthorsAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(uri + "/api/Author");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }

            string message = await response.Content.ReadAsStringAsync();
            List<Author> list = JsonSerializer.Deserialize<List<Author>>(message);
            return list;
        }

        public async Task RemoveBookAsync(int Id)
        {
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync($"{uri}/api/Book/{Id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task AddBookAsync(Book book, int id)
        {
            string reesultAsJson = JsonSerializer.Serialize(book);
            HttpContent content = new StringContent(reesultAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri + "/Book/" + id, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task AddAuthorAsync(Author author)
        {
            string reesultAsJson = JsonSerializer.Serialize(author);
            HttpContent content = new StringContent(reesultAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri + "/api/Author", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
