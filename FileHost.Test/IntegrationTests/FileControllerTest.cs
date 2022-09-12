using FileHost.WebApi.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xunit;

namespace FileHost.Test.IntegrationTests
{
    public class FileControllerTest
    {
        private readonly HttpClient _httpClient = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { }).CreateClient();

        [Fact]
        public async void Successfull_Create_File_In_Database()
        {
            //Arrange
            var file = new FileData
            {
                FileName = "test",
                Size = 23,
                Format = "test",
                Source = "test",
                Data = new byte[] { 74, 65, 73, 74 }
            };

            var json = JsonConvert.SerializeObject(file);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("api/file", content);

            var text = response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async void Successfull_Get_File_By_Id()
        {
            //Arrange
            string id = "8B59CF";

            //Act
            var response = await _httpClient.GetAsync($"api/file/{id}");
            var result = await response.Content.ReadAsStringAsync();
            var file = JsonConvert.DeserializeObject<Response<FileData>>(result);

            //Assert
            Assert.NotNull(file);
            Assert.True(file.Success);
        }
    }
}
