using Repository;

namespace RespositoryTests
{
    public static class TestHelper
    {
        public static Task GetTestTask()
        {
            return new Task() { id = 123, done = false, title = "Test Title for POST", description = "Test Description for POST" };
        }

        public static SingleTaskResult GetTestSingleTaskResult()
        {
            SingleTaskResult taskResult = new SingleTaskResult();

            taskResult.Task = new Task() { id = 123, done = false, title = "Test Title", description = "Test Description" };

            return taskResult;
        }
    }
}