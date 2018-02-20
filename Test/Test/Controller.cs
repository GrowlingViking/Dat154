using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;

namespace Test
{
    class Controller
    {
        private static ServiceTask activeTask;
        private static List<ServiceTask> tasks = new List<ServiceTask>();

        public static void GetAllTasksFromTheWeb()
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://localhost:50094/api/Task/");
            GetTaskListAsync(client, uri);
        }

        public static List<ServiceTask> GetTasks(string type)
        {
            return tasks.FindAll(t => t.ServiceType == type);
        }

        private static async void GetTaskListAsync(HttpClient client, Uri uri)
        {
            string result;
            try
            {
                result = await client.GetStringAsync(uri);
            }
            catch (Exception e)
            {

                throw e;
            }

            tasks = JsonConvert.DeserializeObject<List<ServiceTask>>(result);
        }

        public static string GetStatusOnActive()
        {
            return activeTask.Status;
        }

        public static void SetActiveTasks(ServiceTask task)
        {
            activeTask = task;
        }

        public static ServiceTask GetActiveTask()
        {
            return activeTask;
        }


    }
}
