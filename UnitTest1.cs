
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /*

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
        

        [TestMethod]
        public void OnAddingNewDatatoEmployeeList()
        {
            RestRequest request = new RestRequest("/employees",Method.POST);
            JObject jObject = new JObject();
            jObject.Add("name", "VK");
            jObject.Add("salary", 80000);

            request.AddParameter("application/json", jObject, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);

            Assert.AreEqual("VK", dataResponse.name);
            Assert.AreEqual(80000, dataResponse.salary);

        }

        */
        [TestMethod]
        public void OnAddingMultipleNewDatatoEmployeeList()
        {
            List<Employee> employeeList = new List<Employee>();

            employeeList.Add(new Employee { name = "VK", salary = 80000 });
            employeeList.Add(new Employee { name = "MSD", salary = 100000 });

            foreach(var item in employeeList)
            {
                RestRequest request = new RestRequest("/employees", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("name", item.name);
                jObject.Add("salary", item.salary);

                request.AddParameter("application/json", jObject, ParameterType.RequestBody);


                IRestResponse response = client.Execute(request);

                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);

                Assert.AreEqual(item.name, dataResponse.name);
                Assert.AreEqual(item.salary, dataResponse.salary);
            }
            

        }
    }
}
