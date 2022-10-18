using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Server.Utilities
{
    internal class JsonDataController<T> : IDataController<T>
    {
        private string _path;
        public JsonDataController(string path, string fileName) => _path = Path.Combine(path, fileName);


        public List<T> ReadData()
        {
            string json = File.ReadAllText(_path);

            if (json.Equals(String.Empty))
                throw new InvalidOperationException($"Cannot read data from file {_path}");

            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public void SaveData(List<T> data)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText("date.txt", json);
        }


    }
}
