
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharpTesting
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:4000");
        }

        private IRestResponse getEmployeeList()
        {
            //arrange
            RestRequest request = new RestRequest("/employees", Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            return response;
        }


        [TestMethod]
        public void OnCallingReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(10, dataResponse.Count);

            foreach(var item in dataResponse)
            {
                Console.WriteLine("ID: " + item.id + " Name: " + item.name + " Salary: " + item.salary);
            }
        }
    }
}
