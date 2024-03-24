using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TimeTracking.Models.Database
{
    public class Database<T> : IDatabase<T> where T : BaseEntity
    {
        private string _folderPath;
        private string _filePath;

        public Database()
        {
            _folderPath = @"..\..\..\Database";
            _filePath = _folderPath + $@"\{typeof(T).Name}.json";

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public Database(string fileName)
        {
            _folderPath = @"..\..\..\Database";
            _filePath = _folderPath + $@"\{fileName}.json";

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        private List<T> ReadFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    string content = sr.ReadToEnd();
                    List<T> result = JsonConvert.DeserializeObject<List<T>>(content);

                    return result ?? new List<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error happend during the storage / reading operation");
            }
        }
        private void SaveToFile(List<T> items)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath))
                {
                    string jsonContent = JsonConvert.SerializeObject(items);
                    sw.WriteLine(jsonContent);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error happend during the storage / saving operation");
            }
        }

        public List<T> GetAll()
        {
            List<T> _dataset = ReadFromFile();
            return _dataset;
        }

        public T GetById(int id)
        {
            List<T> _dataset = ReadFromFile();
            T item = _dataset.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Entity with id {id} is not found");
            }

            return item;
        }

        public int Insert(T entity)
        {
            List<T> _dataset = ReadFromFile();
            _dataset.Add(entity);
            SaveToFile(_dataset);
            return entity.Id;
        }

        public void Remove(T entity)
        {
            List<T> _dataset = ReadFromFile();
            T item = _dataset.FirstOrDefault(x => x.Id == entity.Id);

            if (item == null)
            {
                throw new Exception($"Entity with id {entity.Id} is not found");
            }

            _dataset.Remove(item);
            SaveToFile(_dataset);
        }

        public void RemoveById(int id)
        {
            List<T> _dataset = ReadFromFile();
            T item = _dataset.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Entity with id {id} is not found");
            }

            _dataset.Remove(item);
            SaveToFile(_dataset);
        }
        public void Update(T entity)
        {
            List<T> _dataset = ReadFromFile();

            T item = _dataset.FirstOrDefault(x => x.Id == entity.Id);

            if (item == null)
            {
                throw new Exception($"Entity with id {entity.Id} is not found");
            }

            int indexOfElement = _dataset.IndexOf(item);
            _dataset[indexOfElement] = entity;
            SaveToFile(_dataset);
        }
    }
}
