using System;
using System.Collections.Generic;
using Zaharchyk.TaskPlanner.Domain.Models;
using Zaharchyk.TaskPlanner.Domain.Models.Enums;
using Zaharchyk.TaskPlanner.Domain.Logic;
namespace Zaharchyk.TaskPlanner.ConsoleRunner
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var workItemsList = InputWorkItems();
            var taskPlanner=new SimpleTaskPlanner();
            var sortWorkItems = taskPlanner.CreatePlan(workItemsList);
            PrintRezult(sortWorkItems);
        }
        private static WorkItem[] InputWorkItems()
        {
            var workItems = new List<WorkItem>();
            bool isNext = true;
            while (isNext)
            {
                var item = InputItem();
                workItems.Add(item);
                isNext = EnterOneMoreItem();
            }
            return workItems.ToArray();
        }
        private static bool EnterOneMoreItem()
        {
            Console.WriteLine("Enter another item: \n\t->1.Yes\n\t->2.No");
            var response = Console.ReadLine();
            if (response.Equals("1")) return true;
            else return false;
        }
        private static WorkItem InputItem()
        {
            WorkItem workItem = new WorkItem();

            Console.WriteLine("Enter title:");
            workItem.Title=Console.ReadLine();

            Console.WriteLine("Enter description:");
            workItem.Description= Console.ReadLine();

            Console.WriteLine("Enter Priority(num) \n" +
                          "\t-->0. None \n" +
                          "\t-->1. Low \n" +
                          "\t-->2. Medium \n" +
                          "\t-->3. High \n" +
                          "\t-->4. Urgent");

            string priority = Console.ReadLine();
            if (priority != null)
            {
                int temp = int.Parse(priority);

                if (temp > 0 && temp <= 4)
                {
                    workItem.Priority = (Priority)int.Parse(priority);
                }
                else
                {
                    workItem.Priority = 0;
                }
            }
            workItem.Complexity = Complexity.None;
            Console.WriteLine("Enter DueDate:");
            Console.WriteLine("-->Enter day:"); int day = int.Parse(Console.ReadLine());
            Console.WriteLine("-->Enter mounth:"); int mnth= int.Parse(Console.ReadLine());
            Console.WriteLine("-->Enter year:"); int year = int.Parse(Console.ReadLine());
            DateTime inputtedDate = new DateTime(year, mnth, day);workItem.DueDate = inputtedDate;
            workItem.IsCompleted = false;
            workItem.CreationDate = DateTime.Now;
            return workItem;
        }
        private static void PrintRezult(WorkItem[] list)
        {
            foreach (var i in list){Console.WriteLine(i);}
        }


    }
}
