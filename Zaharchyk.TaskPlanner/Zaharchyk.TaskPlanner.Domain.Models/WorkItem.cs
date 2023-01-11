using System;
using Zaharchyk.TaskPlanner.Domain.Models.Enums;

namespace Zaharchyk.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public Guid id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public WorkItem Clone()
        {
            var clone = this;
            return clone;
        }
        public override string ToString(){   
            return $"{Title}: complete due date: {DueDate:dd.MM.yyyy}, priority: {Priority.ToString().ToUpper()}, is complete: {IsCompleted} ";
        }

    }
}
