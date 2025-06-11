using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BNF
{
    public class SaveManager// : SingletonPersistent<SaveManager>
    {
        static Dictionary<string, string> data = new Dictionary<string, string>();

        static string fileName = "save.txt"; // Change it with the next update

        static bool fileLoaded = false;


        

        // private void HandleOnSceneLoaded(Scene scene, LoadSceneMode mode)
        // { Debug.Log(System.IO.Path.Combine(Application.persistentDataPath, fileName));
        //     //savables.Clear();
        //     // if (GameManager.Instance.IsGameScene())
        //     // {
        //     //     var list = new List<MonoBehaviour>(FindObjectsOfType<MonoBehaviour>(true)).Where(m => m is ISavable);
        //     //     savables = new List<GameObject>();
        //     //     foreach (var l in list)
        //     //         savables.Add(l.gameObject);
        //     // }
        //     ReadFromFile();
        // }

        static void WriteToFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in data.Keys)
            {
                sb.AppendLine($"{key}:{data[key]}");
            }

            string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
            System.IO.File.WriteAllText(System.IO.Path.Combine(Application.persistentDataPath, fileName), sb.ToString());


        }

        static void ReadFromFile()
        {
            if (!SaveGameExists())
                WriteToFile();


            string[] lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            foreach (string line in lines)
            {
                string[] s = line.Split(":");
                string key = s[0];
                string value = s[1];
                if (!data.ContainsKey(key))
                    data.Add(key, "");
                data[key] = value;
            }
            fileLoaded = true;
        }

        

        static bool SaveGameExists()
        {
            return System.IO.File.Exists(System.IO.Path.Combine(Application.persistentDataPath, fileName));
        }

        public static bool TryGetCachedValue(string code, out string value)
        {
            if (!fileLoaded) ReadFromFile();

            if (data.ContainsKey(code))
            {
                value = data[code];
                return true;
            }
            else
            {
                value = "";
                return false;
            }

        }
          

    }
}