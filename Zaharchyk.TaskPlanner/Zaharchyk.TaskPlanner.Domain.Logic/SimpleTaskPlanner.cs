using System.Collections.Generic;
using System.Linq;
using Zaharchyk.TaskPlanner.Domain.Models;
namespace Zaharchyk.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] listOFItems)
        {
            var list = listOFItems.ToList();
            var sorted = GoSort(list);
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
