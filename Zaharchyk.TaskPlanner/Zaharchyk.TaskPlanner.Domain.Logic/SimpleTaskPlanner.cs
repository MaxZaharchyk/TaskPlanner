using System.Collections.Generic;
using System.Linq;
using Zaharchyk.TaskPlanner.Domain.Models;
using Zaharchyk.TaskPlanner.DataAccess.Abstractions;
using System;

namespace Zaharchyk.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private IWorkItemsRepository RepositotyItems;
        public SimpleTaskPlanner(IWorkItemsRepository repository)
        {
            RepositotyItems = repository;
        }
        public WorkItem[] CreatePlan()
        {
            var list = RepositotyItems.GetAll();
            var sorted = GoSort(list.ToList());
            return sorted.ToArray();
        }

        private static List<WorkItem> GoSort(List<WorkItem> list)
        {
            list.RemoveAll(it => it.IsCompleted);
            var sortList = list.OrderByDescending(x => (int)(x.Priority))
                .ThenBy(x => x.DueDate)
                .ThenBy(x => x.Title).ToList();
            return sortList;
        }
    }
}
