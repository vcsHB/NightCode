using System;
using System.IO;
using Unity.AppUI.UI;
using UnityEngine;

namespace Core.DataControl
{
    public class JsonLoadHelper<T> where T : new()
    {
        private readonly string _path;

        public JsonLoadHelper(string path)
        {
            _path = path;
        }

        public void Save(T save)
        {
            string json = JsonUtility.ToJson(save);
            File.WriteAllText(_path, json);
        }

        public T Load()
        {
            if(File.Exists(_path) == false)
            {
                T save = new T();
                Save(save);
                return save;
            }
            string json = File.ReadAllText(_path);
            return JsonUtility.FromJson<T>(json);
        }

        public void ResetData()
        {
            File.Delete(_path);
        }
    }
    
}
