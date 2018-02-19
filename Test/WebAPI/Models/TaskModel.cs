using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class TaskModel
    {
        private static List<Task> tasks = new List<Task>()
        {
            new Task { ID = 0, ServiceType = "Cleaning", Status = "New", RoomNr = 2 },
            new Task { ID = 1, ServiceType = "Maintenance", Status = "New", RoomNr = 3 },
            new Task { ID = 2, ServiceType = "Service", Status = "New", RoomNr = 1 },
            new Task { ID = 3, ServiceType = "Maintenance", Status = "New", RoomNr = 5 },
            new Task { ID = 4, ServiceType = "Cleaning", Status = "New", RoomNr = 4 }
        };

        public static void CreateTask(Task task)
        {
            tasks.Add(task);
        }

        public static List<Task> GetAllTasks()
        {
            return tasks;
        }

        public static Task GetTask(int id)
        {
            return tasks.Find(t => t.ID == id);
        }

        public static void UpdateTask(int id, Task task)
        {
            tasks.Remove(tasks.Find(t => t.ID == id));
            tasks.Add(task);
        }

        public static void DeleteTask(int id)
        {
            tasks.Remove(tasks.Find(t => t.ID == id));
        }

    }

    public class Task
    {
        public int ID { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; }
        public int RoomNr { get; set; }
    }
}