using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zaharchyk.TaskPlanner.DataAccess.Abstractions;
using Zaharchyk.TaskPlanner.Domain.Models;

namespace Zaharchyk.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private readonly string PathToFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "work-items.json");
        private static Dictionary<Guid, WorkItem> Idictionary = new Dictionary<Guid, WorkItem>();
        private static ref readonly Dictionary<Guid, WorkItem> dictionary => ref Idictionary;

        public FileWorkItemsRepository()
        {
            try
            {
                var input = File.ReadAllText(PathToFile);
                var inputItems = JsonConvert.DeserializeObject<WorkItem[]>(input);
                if (inputItems.Length != 0) { AddToDictionary(inputItems); }
            }
            catch (FileNotFoundException e)
            {
                File.Create(PathToFile);
            }
        }
        private void AddToDictionary(WorkItem[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Idictionary.Add(array[i].id, array[i]);
        }
        public Guid Add(WorkItem workItem)
        {
            var clonedItem = workItem.Clone();
            clonedItem.id = Guid.NewGuid();
            Idictionary.Add(clonedItem.id, clonedItem);
            return workItem.id;
        }
        public WorkItem Get(Guid id)
        {
            throw new NotImplementedException();
        }
        public WorkItem[] GetAll()
        {
            var jSonWorkItems = File.ReadAllText(PathToFile);
            var arrayItems = JsonConvert.DeserializeObject<WorkItem[]>(jSonWorkItems);
            if (arrayItems != null)
            {
                return arrayItems;
            }
            else
            {
                return new WorkItem[0];
            }
        }
        public bool Update(WorkItem workItem)
        {
            return Idictionary[workItem.id].IsCompleted = true;
        }
        public bool Remove(Guid id)
        {
            return Idictionary.Remove(id);
        }
        public void SaveChanges()
        {
            var ListItems = dictionary.Values.ToList();
            var jsonFile = JsonConvert.SerializeObject(ListItems);
            File.WriteAllText(PathToFile, jsonFile);
        }
    }
}
