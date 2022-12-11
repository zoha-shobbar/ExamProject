using ExamProject.Application.Dtos.Inputs;
using ExamProject.Application.Response;
using ExamProject.Domain.Entities;
using ExamProject.Infrastructure.Identity.Models;
using Intsoft.Exam.Application.Dtos.Inputs;
using Newtonsoft.Json;
using System.Text;

namespace ExamProject.IntegrationTest.ApiTests
{
    public class BookApiTests : BaseTest
    {
        [Fact]
        public async Task GetTest()
        {
            var response = await TestClient.GetAsync("/api/book");

            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResponse<Book>>(serializedResponse);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ResponseStatus.Sucsess, result.Status);
        }

        [Theory]
        [InlineData("123456789", "Kian", 2022, "SQL", "SQL Servar on action")]
        public async Task PostTest(string isbn, string publisher, int publishyear, string summury, string title)
        {
            var book = new BookInput
            {
                ISBN=isbn,
                Publisher=publisher,
                PublishYear=publishyear,
                Summury=summury,
                Title=title
            };

            var json = JsonConvert.SerializeObject(book);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync("/api/book", stringContent);

            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SingleResponse<Book>>(serializedResponse);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ResponseStatus.Sucsess, result.Status);
            Assert.True(result.Result.Id>0);
        }

        [Theory]
        [InlineData("123456789", "Kian", 2022, "SQL", "SQL Servar on action", "123456789", "Kian new", 2020, "SQL new", "SQL Servar on action new")]
        public async Task PutTest(string initIsbn, string initPublisher, int initPublishyear, string initSummury, string initTitle,
            string isbn, string publisher, int publishyear, string summury, string title)
        {
            var book = new BookInput
            {
                ISBN=isbn,
                Publisher=publisher,
                PublishYear=publishyear,
                Summury=summury,
                Title=title
            };

            var createJson = JsonConvert.SerializeObject(book);
            var createStringContent = new StringContent(createJson, UnicodeEncoding.UTF8, "application/json");

            var createResponse = await TestClient.PostAsync("/api/book", createStringContent);

            createResponse.EnsureSuccessStatusCode();

            var createSerializedResponse = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<SingleResponse<Book>>(createSerializedResponse);

            //update book
            var updateBook = new BookInput
            {
                ISBN=isbn,
                Publisher=publisher,
                PublishYear=publishyear,
                Summury=summury,
                Title=title
            };

            var updateJson = JsonConvert.SerializeObject(updateBook);
            var updateStringContent = new StringContent(updateJson, UnicodeEncoding.UTF8, "application/json");

            var updateResponse = await TestClient.PutAsync($"/api/book/{createResult.Result.Id}", updateStringContent);

            updateResponse.EnsureSuccessStatusCode();

            var updateSerializedResponse = await updateResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<SingleResponse<Book>>(updateSerializedResponse);

            Assert.Equal(createResult.Result.Id, updateResult.Result.Id);
            Assert.Equal(createResult.Result.ISBN, updateResult.Result.ISBN);
            Assert.Equal(createResult.Result.PublishYear, updateResult.Result.PublishYear);
            Assert.Equal(createResult.Result.Publisher, updateResult.Result.Publisher);
            Assert.Equal(createResult.Result.Summury, updateResult.Result.Summury);
            Assert.Equal(createResult.Result.Title, updateResult.Result.Title);

        }

        [Theory]
        [InlineData("123456789", "Kian", 2016, "SQL", "SQL Servar on action")]
        public async Task DeleteTest(string isbn, string publisher, int publishyear, string summury, string title)
        {
            var book = new BookInput
            {
                ISBN=isbn,
                Publisher=publisher,
                PublishYear=publishyear,
                Summury=summury,
                Title=title
            };

            var json = JsonConvert.SerializeObject(book);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync("/api/book", stringContent);

            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SingleResponse<Book>>(serializedResponse);

            Assert.True(result.Result.Id>0);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ResponseStatus.Sucsess, result.Status);

            ///delete action
            var deleteResponse = await TestClient.DeleteAsync($"/api/book/{result.Result.Id}");

            deleteResponse.EnsureSuccessStatusCode();

            Assert.Equal(System.Net.HttpStatusCode.OK, deleteResponse.StatusCode);
            var deleteSerializedResponse = await deleteResponse.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<SingleResponse<bool>>(deleteSerializedResponse);

            Assert.Equal(System.Net.HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.True(deleteResult.Result);
            Assert.Equal(ResponseStatus.Sucsess, deleteResult.Status);
        }

        [Theory]
        [InlineData("123456789", "Kian", 1018, "SQL", "SQL Servar on action")]
        public async Task ArchivTest(string isbn, string publisher, int publishyear, string summury, string title)
        {
            var book = new BookInput
            {
                ISBN=isbn,
                Publisher=publisher,
                PublishYear=publishyear,
                Summury=summury,
                Title=title
            };

            var createJson = JsonConvert.SerializeObject(book);
            var createStringContent = new StringContent(createJson, UnicodeEncoding.UTF8, "application/json");

            var createResponse = await TestClient.PostAsync("/api/book", createStringContent);

            createResponse.EnsureSuccessStatusCode();

            var createSerializedResponse = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<SingleResponse<Book>>(createSerializedResponse);

            //update book
            var updateResponse = await TestClient.PutAsync($"/api/book/Archive/{createResult.Result.Id}",null);

            updateResponse.EnsureSuccessStatusCode();

            var updateSerializedResponse = await updateResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<SingleResponse<bool>>(updateSerializedResponse);

            Assert.True(updateResult.Result);
            Assert.Equal(ResponseStatus.Sucsess, updateResult.Status);
        }


    }
}
