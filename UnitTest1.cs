
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
        RestClient client;                                                      //Declaration of Client

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:4000");                      //Setting Link for the Client 
        }

        private IRestResponse getEmployeeList()
        {
            //arrange
            RestRequest request = new RestRequest("/employees", Method.GET);              //Retrieving EmployeesList 

            //act
            IRestResponse response = client.Execute(request);
            return response;
        }
        

        [TestMethod]
        public void OnCallingReturnEmployeeList()                                               
        {
            IRestResponse response = getEmployeeList();

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);                                 //Checking the Status
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);          //Desrializing Object
            Assert.AreEqual(10, dataResponse.Count);                                                          //Counting the responses that matches


            //Retrieving the data from the DataResponse 
            foreach(var item in dataResponse)
            {
                Console.WriteLine("ID: " + item.id + " Name: " + item.name + " Salary: " + item.salary);
            }
        }
        

        [TestMethod]
        public void OnAddingNewDatatoEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees",Method.POST);                          //For Adding Data to EmployeeList
            JObject jObject = new JObject();
           
            //Act

            jObject.Add("name", "VK");                                                                  //Addition of Data
            jObject.Add("salary", 80000);

            request.AddParameter("application/json", jObject, ParameterType.RequestBody);                 //Validation of RestRequest


            IRestResponse response = client.Execute(request);                                            //Executing the response


            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);                                //Checking Validation
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);           //Deserializing Object

            Assert.AreEqual("VK", dataResponse.name);                                                   
            Assert.AreEqual(80000, dataResponse.salary);

        }


        [TestMethod]
        public void OnAddingMultipleNewDatatoEmployeeList()
        {
            //Arrange
            List<Employee> employeeList = new List<Employee>();

            employeeList.Add(new Employee { name = "VK", salary = 80000 });                         //For Multiple Adding Data to EmployeeList
            employeeList.Add(new Employee { name = "MSD", salary = 100000 });

            foreach (var item in employeeList)
            {
                //Act
                RestRequest request = new RestRequest("/employees", Method.POST);                  //For Adding Data to EmployeeList
                JObject jObject = new JObject();
                jObject.Add("name", item.name);
                jObject.Add("salary", item.salary);

                request.AddParameter("application/json", jObject, ParameterType.RequestBody);          //Validation of RestRequest


                IRestResponse response = client.Execute(request);                                   //Executing the response


                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);                               //Checking Validation
                Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);          //Deserializing Object

                Assert.AreEqual(item.name, dataResponse.name);
                Assert.AreEqual(item.salary, dataResponse.salary);
            }
        }
            


        [TestMethod]

        public void UpdatingDatainEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("employees/13", Method.PUT);
            JObject jobject = new JObject();
         
            jobject.Add("name", "Virat");
            jobject.Add("salary", 150000);
            
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            
            //Act
            IRestResponse response = client.Execute(request);
            

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);

            Assert.AreEqual(dataResponse.name, "Virat");
            Assert.AreEqual(dataResponse.salary, 150000);
        }


        [TestMethod]

        public void DeleteDatainEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("employees/13", Method.DELETE);
            //Act
            IRestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
