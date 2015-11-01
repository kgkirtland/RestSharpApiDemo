using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Repository
{
    public class TaskApiRepository : ITaskApiRepository
    {
        private readonly IRestClient restClient;

        private const string taskApiBasePath = "api/tasks";

        public TaskApiRepository(IRestClient restClient)
        {            
            this.restClient = restClient;
        }

        public HttpStatusCode PostTask(Task task)
        {
            IRestRequest request = new RestRequest(taskApiBasePath, Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(task);

            IRestResponse<TaskList> response = this.restClient.Execute<TaskList>(request);

            if (response == null)
            {
                throw new Exception("Response is null");
            }

            return response.StatusCode;            
        }

        public List<Task> GetTasks()
        {
            IRestRequest request = new RestRequest(taskApiBasePath, Method.GET);
            IRestResponse<TaskList> response = this.restClient.Execute<TaskList>(request);
            List<Task> taskList = new List<Task>();

            if (response != null && response.Data != null)
            {
                taskList = response.Data.Tasks;
            }

            return taskList;
        }

        public Task GetTaskById(int id)
        {
            string taskApiPath = taskApiBasePath + "/{id}";
            IRestRequest request = new RestRequest(taskApiPath, Method.GET);

            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = this.restClient.Execute<SingleTaskResult>(request).Data;
            Task task = response != null ? response.Task : null;
            
            return task;
        }
    }
}
