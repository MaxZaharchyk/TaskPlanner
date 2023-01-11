using System;
using System.Collections.Generic;
using Zaharchyk.TaskPlanner.Domain.Models;
using Zaharchyk.TaskPlanner.Domain.Models.Enums;
using Zaharchyk.TaskPlanner.Domain.Logic;
using Zaharchyk.TaskPlanner.DataAccess.Abstractions;
using Zaharchyk.TaskPlanner.DataAccess;
using System.Linq;

namespace Zaharchyk.TaskPlanner.ConsoleRunner
{
    internal static class Program
    {
        private static IWorkItemsRepository iRepository = new FileWorkItemsRepository();
        private static List<WorkItem> arrayItems = new List<WorkItem>();
        private static SimpleTaskPlanner iPlanner = new SimpleTaskPlanner(new FileWorkItemsRepository()); 
        
        public static void Main(string[] args)
        {
            GenerateList();
            while (true)
            {
                Console.WriteLine("MENU \n" +
                                  "\t-->1. Add new work item\n" +
                                  "\t-->2. Print work items\n" +
                                  "\t-->3. Mark work item as completed\n" +
                                  "\t-->4. Remove a work item\n" +
                                  "\t-->5. Create Plan\n" +
                                  "\t-->7. Quit the app");
                switch (Console.ReadLine())
                {
                    case "1":{InputItem();break;}
                    case "2":{PrintContainer();break;}
                    case "3":{CompleteTask();break;}
                    case "4":{remove();break;}
                    case "5":{buildPlan();break;}
                    case "7":{exit();return;}
                    default:{Console.WriteLine("This command doesn't exist");break;}
                }
            }
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
            workItem.id = Guid.NewGuid();
            iRepository.Add(workItem);
            GenerateList();
            return workItem;
        }
        private static void PrintContainer()
        {
            var list = iRepository.GetAll();
            if (list.Length > 0){
                for (int i = 0; i < list.Length; i++){
                    Console.WriteLine($"{i + 1}. {list[i].ToString()}");
                }
            }else{
                Console.WriteLine("Tasks container is empty!");
            }
        }
        private static void ReNewContainer()
        {
            iRepository.SaveChanges();
            GenerateList();
        }
        private static void CompleteTask()
        {
            PrintContainer();
            if (arrayItems.Count > 0)
            {
                Console.WriteLine("Makr task, which was completed:");
                var input = Console.ReadLine();
                try
                {
                    var c = int.Parse(input);
                    if (c > arrayItems.Count)
                    {
                        Console.WriteLine("Incorrect number of task. Try again!");
                    }
                    else
                    {
                        iRepository.Update(arrayItems[c - 1]);
                        GenerateList();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect number of task. Try again!");
                }
            }
        }
        private static void remove()
        {
            PrintContainer();
            if (arrayItems.Count > 0)
            {
                Console.WriteLine("Makr task, which should be removed:");
                var input = Console.ReadLine();
                try
                {
                    var c = int.Parse(input);
                    if (c > arrayItems.Count)
                    {
                        Console.WriteLine("Incorrect number of task. Try again!");
                    }
                    else
                    {
                        iRepository.Remove(arrayItems[c - 1].id);
                        GenerateList();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect number of task. Try again!");
                }
            }
        }
        private static void GenerateList()
        {
            arrayItems = iRepository.GetAll().ToList();
        }
        private static void exit()
        {
            Console.WriteLine("Do you want to save all changes&\n" +
                          "1. Yes\n" +
                          "2. No");
            var c = Console.ReadLine();
            if (c == "1")
            {
                iRepository.SaveChanges();
                GenerateList();
            }
        }
        private static void buildPlan()
        {
            var builder = iPlanner.CreatePlan();
            foreach (var i in builder)
            {
                Console.WriteLine(i.ToString());
            }
        }

    }
}