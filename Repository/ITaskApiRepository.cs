using System.Collections.Generic;
using System.Net;

namespace Repository
{
    public interface ITaskApiRepository
    {
        Task GetTaskById(int id);
        List<Task> GetTasks();
        HttpStatusCode PostTask(Task task);
    }
}
