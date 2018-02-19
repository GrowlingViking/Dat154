using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    public class TaskController : ApiController
    {
        // GET api/values
        public IEnumerable<Task> Get()
        {
            return TaskModel.GetAllTasks();
        }

        // GET api/values/5
        public Task Get(int id)
        {
            return TaskModel.GetTask(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            var task = JsonConvert.DeserializeObject<Task>(value);
            TaskModel.CreateTask(task);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            var task = JsonConvert.DeserializeObject<Task>(value);
            TaskModel.UpdateTask(id, task);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            TaskModel.DeleteTask(id);
        }
    }
}
