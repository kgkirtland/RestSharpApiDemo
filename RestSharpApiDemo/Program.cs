using Ninject;
using Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RestSharpApiDemo
{
    class Program
    {
        private static ITaskApiRepository taskRepository;
        private static IRestClient restClient;

        static void Main(string[] args)
        {
            restClient = GetRestClient();            
            
            //TestPost();
            //TestGet();
            TestGetById(123);

            Console.ReadKey();
        }

        private static void TestPost()
        {
            try 
            {
                taskRepository = new TaskApiRepository(restClient);
           
                Task task = new Task() { id = 123, done = false, title = "test for post", description = "Test for post xxx" };

                var response = taskRepository.PostTask(task);

                Console.WriteLine("Response Status: {0}", response.ToString());
            }
            catch (Exception exception)
            {
                DisplayException(exception);
            }            
        }

        private static void TestGet()
        {
            try 
            {
                taskRepository = new TaskApiRepository(restClient);

                List<Task> tasks = taskRepository.GetTasks();

                foreach (var task in tasks)
                {
                    Console.WriteLine("\nId: {0}\nTitle: {1}\nDescription: {2}\nDone: {3}", task.id, task.title, task.description, task.done);
                }
            }
            catch (Exception exception)
            {
                DisplayException(exception);
            }
        }

        private static void TestGetById(int id)
        {
            try
            {
                taskRepository = new TaskApiRepository(restClient);

                Task task = taskRepository.GetTaskById(id);

                if (task != null)
                {
                    Console.WriteLine("\nId: {0}\nTitle: {1}\nDescription: {2}\nDone: {3}", task.id, task.title, task.description, task.done);
                }
                else
                {
                    Console.WriteLine("Task not found.");

                }
            }
            catch (Exception exception)
            {
                DisplayException(exception);
            }
        }

        private static IRestClient GetRestClient()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            return kernel.Get<IRestClient>();
        }

        private static void DisplayException(Exception exception)
        {
            Console.WriteLine(string.Format("Exception: {0}", exception.ToString()));
        }
    }
}
