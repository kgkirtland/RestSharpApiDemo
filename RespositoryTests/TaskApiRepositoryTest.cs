using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RespositoryTests
{
    [TestClass]
    public class TaskApiRepositoryTest
    {
        private Mock<IRestClient> mockedRestClient = new Mock<IRestClient>();

        #region "PostTask Tests"
        [TestMethod]
        public void PostTaskTest_VerifyResponseNotNull()
        {
            IRestResponse<TaskList> restResponse = new RestResponse<TaskList>();

            mockedRestClient.Setup(x => x.Execute<TaskList>(It.IsAny<IRestRequest>())).Returns(restResponse);
            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            Task task = GetTestTask();

            HttpStatusCode response = taskApiRepository.PostTask(task);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void PostTaskTest_ResponseNull_VerifyExceptionThrown()
        {
            IRestResponse<TaskList> restResponse = null;
            bool exceptionThrown = false;

            mockedRestClient.Setup(x => x.Execute<TaskList>(It.IsAny<IRestRequest>())).Returns(restResponse);
            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            Task task = GetTestTask();

            try
            { 
                HttpStatusCode response = taskApiRepository.PostTask(task);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void PostTaskTest__VerifyReturnsResponseStatusCodeIsCreated()
        {
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;
            HttpStatusCode actualStatusCode;

            IRestResponse<TaskList> restResponse = new RestResponse<TaskList>();
            restResponse.StatusCode = HttpStatusCode.Created;

            mockedRestClient.Setup(x => x.Execute<TaskList>(It.IsAny<IRestRequest>())).Returns(restResponse);

            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            Task task = GetTestTask();

            actualStatusCode = taskApiRepository.PostTask(task);
            
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        [TestMethod]
        public void PostTaskTest__VerifyExecuteCalledOnce()
        {
            IRestResponse<TaskList> restResponse = new RestResponse<TaskList>();

            mockedRestClient.Setup(x => x.Execute<TaskList>(It.IsAny<IRestRequest>())).Returns(restResponse);

            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            Task task = GetTestTask();

            var response = taskApiRepository.PostTask(task);

            mockedRestClient.Verify(x => x.Execute<TaskList>(It.IsAny<IRestRequest>()), Times.Once());
        }
        #endregion

        #region "GetTasks Tests"
        [TestMethod]
        public void GetTasksTest_VerifyTasksNotNull()
        {
            IRestResponse<TaskList> restResponse = new RestResponse<TaskList>();

            mockedRestClient.Setup(x => x.Execute<TaskList>(It.IsAny<IRestRequest>())).Returns(restResponse);

            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            List<Task> taskList = taskApiRepository.GetTasks();

            Assert.IsNotNull(taskList);
        }

        [TestMethod]
        public void GetTasksTest_VerifyExecuteCalledOnce()
        {
            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            List<Task> taskList = taskApiRepository.GetTasks();

            mockedRestClient.Verify(x => x.Execute<TaskList>(It.IsAny<IRestRequest>()), Times.Once());
        }

        [TestMethod]
        public void GetTasksTest_Response_Null_VerifyTaskListCountIsZero()
        {
            IRestResponse<TaskList> restResponse = null;

            mockedRestClient.Setup(x => x.Execute<TaskList>(It.IsAny<IRestRequest>())).Returns(restResponse);

            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            List<Task> taskList = taskApiRepository.GetTasks();

            Assert.IsTrue(taskList.Count == 0);
        }
        #endregion

        #region "GetTaskById Tests"
        [TestMethod]
        public void GetByIdTest_Id_123_VerifyTaskNotNull()
        {
            SingleTaskResult result = GetTestSingleTaskResult();
            mockedRestClient.Setup(x => x.Execute<SingleTaskResult>(It.IsAny<IRestRequest>()).Data).Returns(result);
            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            int id = 123;

            Task task = taskApiRepository.GetTaskById(id);

            Assert.IsNotNull(task);
        }

        [TestMethod]
        public void GetByIdTest_Id_123_Returns_Null_VerifyTaskIsNull()
        {
            SingleTaskResult result = null;
            mockedRestClient.Setup(x => x.Execute<SingleTaskResult>(It.IsAny<IRestRequest>()).Data).Returns(result);
            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            int id = 123;

            Task task = taskApiRepository.GetTaskById(id);

            Assert.IsNull(task);
        }

        [TestMethod]
        public void GetByIdTest_Id_123_VerifyExecuteCalledOnce()
        {
            SingleTaskResult result = GetTestSingleTaskResult();
            mockedRestClient.Setup(x => x.Execute<SingleTaskResult>(It.IsAny<IRestRequest>()).Data).Returns(result);
            var taskApiRepository = new TaskApiRepository(mockedRestClient.Object);

            int id = 123;

            Task task = taskApiRepository.GetTaskById(id);

            mockedRestClient.Verify(x => x.Execute<SingleTaskResult>(It.IsAny<IRestRequest>()).Data, Times.Once());
        }
        #endregion        
        
        private Task GetTestTask()
        {
            return new Task() { id = 123, done = false, title = "Test Title for POST", description = "Test Description for POST" };
        }

        private SingleTaskResult GetTestSingleTaskResult()
        {
            SingleTaskResult taskResult = new SingleTaskResult();

            taskResult.Task = new Task() { id = 123, done = false, title = "Test Title", description = "Test Description" };

            return taskResult; 
        }
    }
}
