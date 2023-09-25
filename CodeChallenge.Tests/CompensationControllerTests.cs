
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using CodeChallenge.DTOs;
using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetCompensationByEmployeeID_ReturnsNotFound()
        {
            // Arrange
            var employeeId = "123456ae-edd3-4847-99fe-c4518e821234"; // doesnt exist

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            // Verified payload with postman
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void CreateCompensationTheGetCompensationByEmployeeID_ReturnsOk()
        {
            int expectedSalary = 100000;
            string expectedEmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Create a new CompensationAddDTO
            var compensationNew = new
            {
                EmployeeID = expectedEmployeeId,
                Salary = expectedSalary
            };

            // Create expected responseDTO
            var expectedCompensationResponse = new
            {
                EmployeeID = expectedEmployeeId,
                Salary = expectedSalary
            };

            var requestContent = new JsonSerialization().ToJson(compensationNew);

            // Execute
            var postRequestTask = _httpClient.PostAsync($"api/compensation/",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;

            // Verified payload with postman
            Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode);

        }

        [TestMethod]
        public void GetCompensationByEmployeeID_ReturnsOk()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            // Verified payload with postman
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
