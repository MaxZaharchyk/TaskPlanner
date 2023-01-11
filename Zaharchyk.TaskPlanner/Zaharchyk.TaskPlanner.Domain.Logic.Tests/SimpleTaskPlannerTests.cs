using Moq;
using System;
using Xunit;
using Zaharchyk.TaskPlanner.DataAccess.Abstractions;
using Zaharchyk.TaskPlanner.Domain.Models;
using Zaharchyk.TaskPlanner.Domain.Models.Enums;

namespace Zaharchyk.TaskPlanner.Domain.Logic.Tests
{
    public class SimpleTaskPlannerTests
    {
        private static WorkItem[] GenerateArray()
        {
            var mockOnject = new Mock<IWorkItemsRepository>();
            mockOnject.Setup(rep => rep.GetAll()).Returns(GenerateItemArray());
            SimpleTaskPlanner simpleTaskPlanner = new SimpleTaskPlanner(mockOnject.Object);
            var result = simpleTaskPlanner.CreatePlan();
            return (WorkItem[])result;
        }

        [Fact]
        private void lengthTest()
        {
            var array = GenerateArray();
            Assert.Equal(sortArray().Length, array.Length);
        }

        [Fact]
        private void completeTest()
        {
            var array = GenerateArray();
            foreach (var i in array)
            {
                Assert.False(i.IsCompleted);
            }
        }

        [Fact]
        private void sortTest()
        {
            var array = GenerateArray();
            var sorted = sortArray();
            for (int i = 0; i < array.Length; i++)
                Assert.Equal(sorted[i].id, array[i].id);
        }

        private static WorkItem[] GenerateItemArray()
        {
            var workItems = new WorkItem[]
            {
                new WorkItem
                {
                    id = Guid.Parse("ce611054-5bf0-4811-8f19-d51243ba0b83"),
                    CreationDate = DateTime.Parse("11.01.2023 12:10:25"), DueDate = DateTime.Parse("14.01.2023 17:00:00"),
                    Priority = Priority.Medium, Complexity = Complexity.Weeks, Title = "Lab_01", Description = "First item",
                    IsCompleted = false
                },
                new WorkItem
                {
                    id = Guid.Parse("a711093b-0f76-4cb5-8dc2-27471e1150b0"),
                    CreationDate = DateTime.Parse("11.01.2023 14:10:25"), DueDate = DateTime.Parse("18.01.2023 13:00:00"),
                    Priority = Priority.Medium, Complexity = Complexity.None, Title = "Lab_2", Description = "Second item",
                    IsCompleted = true
                },
                new WorkItem
                {
                    id = Guid.Parse("8667738c-038a-4a4d-b706-9ddd0a2d897c"),
                    CreationDate = DateTime.Parse("13.01.2023 14:10:25"), DueDate = DateTime.Parse("19.01.2023 14:10:25"),
                    Priority = Priority.High, Complexity = Complexity.None, Title = "Lab_3", Description = "Third item",
                    IsCompleted = false
                }
            };
            return workItems;
        }

        private static WorkItem[] sortArray()
        {
            var workItems = new WorkItem[]
            {
                new WorkItem
                {
                    id = Guid.Parse("8667738c-038a-4a4d-b706-9ddd0a2d897c"),
                    CreationDate = DateTime.Parse("13.01.2023 14:10:25"), DueDate = DateTime.Parse("19.01.2023 14:10:25"),
                    Priority = Priority.High, Complexity = Complexity.None, Title = "Lab_3", Description = "Third item",
                    IsCompleted = false
                },
                new WorkItem
                {
                    id = Guid.Parse("ce611054-5bf0-4811-8f19-d51243ba0b83"),
                    CreationDate = DateTime.Parse("11.01.2023 12:10:25"), DueDate = DateTime.Parse("14.01.2023 17:00:00"),
                    Priority = Priority.Medium, Complexity = Complexity.Weeks, Title = "Lab_01", Description = "First item",
                    IsCompleted = false
                }
                
            };
            return workItems;
        }
    }
}
