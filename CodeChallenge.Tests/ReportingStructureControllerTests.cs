
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using CodeChallenge.DTOs;
//using CodeChallenge.Models;

//using CodeCodeChallenge.Tests.Integration.Extensions;
//using CodeCodeChallenge.Tests.Integration.Helpers;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace CodeCodeChallenge.Tests.Integration
//{
//    [TestClass]
//    public class ReportingStructureControllerTests
//    {
//        private static HttpClient _httpClient;
//        private static TestServer _testServer;

//        [ClassInitialize]
//        // Attribute ClassInitialize requires this signature
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
//        public static void InitializeClass(TestContext context)
//        {
//            _testServer = new TestServer();
//            _httpClient = _testServer.NewClient();
//        }

//        [ClassCleanup]
//        public static void CleanUpTest()
//        {
//            _httpClient.Dispose();
//            _testServer.Dispose();
//        }

//        // Direct Copy from Employee
//        [TestMethod]
//        public void GetEmployeeById_Returns_Ok()
//        {
//            // Arrange
//            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
//            var expectedFirstName = "John";
//            var expectedLastName = "Lennon";

//            // Execute
//            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}");
//            var response = getRequestTask.Result;

//            // Assert
//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//            var employee = response.DeserializeContent<Employee>();
//            Assert.AreEqual(expectedFirstName, employee.FirstName);
//            Assert.AreEqual(expectedLastName, employee.LastName);

//            // ADDED FOR DEBUGGING
//            Assert.AreEqual(2, employee.DirectReports.Count);
//        }

//        //[TestMethod]
//        //public void GetReportingStructureByEmployeeId_Returns_Ok()
//        //{
//        //    // Arrange
//        //    var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
//        //    var expectedFirstName = "John";
//        //    var expectedLastName = "Lennon";

//        //    // Execute
//        //    var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
//        //    var response = getRequestTask.Result;

//        //    // Assert
//        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//        //    var reportingStructure = response.DeserializeContent<ReportingStructureSingleDTO>();
//        //    Assert.AreEqual(4, reportingStructure.NumberOfReports);
//        //    Assert.AreEqual(employeeId, reportingStructure.Employee.EmployeeId);
//        //}
//    }
//}
