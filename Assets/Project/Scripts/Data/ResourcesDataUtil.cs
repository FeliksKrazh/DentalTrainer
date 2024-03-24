using UnityEngine;
using System.IO;

namespace DataUtil
{
    public class ResourcesDataUtil
    {
        private static readonly string NAME_SYS = "TargetTransforms";
        private static readonly string NAME_DATA_JSON_FILE = "/" + NAME_SYS + ".json";

        private static string GetFilePath
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsEditor: return Application.streamingAssetsPath;
                    case RuntimePlatform.Android: return Application.persistentDataPath;
                    default: return Application.streamingAssetsPath;
                }
            }
        }

        public static void SaveData<T>(T data)
        {
            string fileDataPath = GetFilePath + NAME_DATA_JSON_FILE;
            string JsonString = JsonUtility.ToJson(data);
            File.WriteAllText(fileDataPath, JsonString);
        }

        public static T LoadData<T>()
        {
            try
            {
                string fileDataPath = GetFilePath + NAME_DATA_JSON_FILE;
                string JSONString = File.ReadAllText(fileDataPath);
                return JsonUtility.FromJson<T>(JSONString);
            }
            catch
            {
                Debug.Log("Error LoadData");
                return default(T);
            }
        }
    }
}

