using System;
using Zaharchyk.TaskPlanner.Domain.Models.Enums;

namespace Zaharchyk.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public override string ToString(){   
            return $"{Title}: виконати до {DueDate:dd.MM.yyyy}, пріоритет: {Priority.ToString().ToUpper()} ";
        }

    }
}
