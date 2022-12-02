using Intsoft.Exam.API.Models.Entities;
using Intsoft.Exam.API.Models.Inputs;

namespace Intsoft.Exam.IntegrationTests
{
    public class UserApiTests : BaseTest
    {
        [Fact]
        public async Task GetTest()
        {
            var response = await TestClient.GetAsync("/api/user");

            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ListResponse<User>>(serializedResponse);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ResponseStatus.Sucsess, result.Status);
        }

        [Theory]
        [InlineData("User 1", "Family", "1234567890", "09355149272")]
        public async Task PostTest(string firstName, string lastName, string nationalCode, string phoneNumber)
        {
            var user = new UserInput
            {
                FirstName =firstName,
                LastName =lastName,
                NationalCode =nationalCode,
                PhoneNumber=phoneNumber
            };

            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync("/api/user", stringContent);

            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SingleResponse<User>>(serializedResponse);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ResponseStatus.Sucsess, result.Status);
            Assert.True(result.Result.Id>0);
        }

        [Theory]
        [InlineData("User 1", "Family", "123", "09355149272")]
        public async Task PostTest_InvalidNationalCode(string firstName, string lastName, string nationalCode, string phoneNumber)
        {
            var user = new UserInput
            {
                FirstName =firstName,
                LastName =lastName,
                NationalCode =nationalCode,
                PhoneNumber=phoneNumber
            };

            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync("/api/user", stringContent);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("User 1", "Family", "1234567890", "01355149272")]
        public async Task PostTest_InvalidPhoneNumber(string firstName, string lastName, string nationalCode, string phoneNumber)
        {
            var user = new UserInput
            {
                FirstName =firstName,
                LastName =lastName,
                NationalCode =nationalCode,
                PhoneNumber=phoneNumber
            };

            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync("/api/user", stringContent);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("User 1", "Family", "1234567890", "09355149272", "new user", "new family", "0987654321", "09355149070")]
        public async Task PutTest(string initFirstName, string initLastName, string initNationalCode, string initPhoneNumber,
                                    string firstName, string lastName, string nationalCode, string phoneNumber)
        {
            var user = new UserInput
            {
                FirstName =initFirstName,
                LastName =initLastName,
                NationalCode =initNationalCode,
                PhoneNumber=initPhoneNumber
            };

            var createJson = JsonConvert.SerializeObject(user);
            var createStringContent = new StringContent(createJson, UnicodeEncoding.UTF8, "application/json");

            var createResponse = await TestClient.PostAsync("/api/user", createStringContent);

            createResponse.EnsureSuccessStatusCode();

            var createSerializedResponse = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<SingleResponse<User>>(createSerializedResponse);

            //update user
            var updateUser = new UserInput
            {
                FirstName =firstName,
                LastName =lastName,
                NationalCode =nationalCode,
                PhoneNumber=phoneNumber
            };

            var updateJson = JsonConvert.SerializeObject(user);
            var updateStringContent = new StringContent(updateJson, UnicodeEncoding.UTF8, "application/json");

            var updateResponse = await TestClient.PutAsync($"/api/user/{createResult.Result.Id}", updateStringContent);

            updateResponse.EnsureSuccessStatusCode();

            var updateSerializedResponse = await updateResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<SingleResponse<User>>(updateSerializedResponse);

            Assert.Equal(createResult.Result.Id, updateResult.Result.Id);
            Assert.Equal(createResult.Result.FirstName, updateResult.Result.FirstName);
            Assert.Equal(createResult.Result.LastName, updateResult.Result.LastName);
            Assert.Equal(createResult.Result.PhoneNumber, updateResult.Result.PhoneNumber);
            Assert.Equal(createResult.Result.NationalCode, updateResult.Result.NationalCode);

        }

    }
}
