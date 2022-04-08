using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SettingsContainer 
{
    public Dictionary<string, string> settings = new Dictionary<string, string>();

    public static SettingsContainer Load(string name = "Settings.txt")
    {
        string path = Application.persistentDataPath + "/" + name;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsContainer settings = formatter.Deserialize(stream) as SettingsContainer;
            stream.Close();
            return settings;
        }

        return new SettingsContainer();
    }

    public void Save(string name = "Settings.txt")
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, this);
        stream.Close();
    }
}
