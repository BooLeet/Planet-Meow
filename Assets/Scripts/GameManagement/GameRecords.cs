using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameRecords
{
    [System.Serializable]
    public class Record
    {
        public int points;
        public float time;

        public Record(int points, float time)
        {
            this.points = points;
            this.time = time;
        }
    }

    private Record currentRecord;

    public int points
    {
        get { return currentRecord != null ? currentRecord.points : 0; }
    }

    public float time
    {
        get { return currentRecord != null ? currentRecord.time : 0; }
    }

    public bool newPointsRecord
    {
        get;
        private set;
    }

    public bool newTimeRecord
    {
        get;
        private set;
    }

    public bool HasRecords()
    {
        return currentRecord != null;
    }

    public bool NewPointsRecord(int points)
    {
        return currentRecord == null || points > currentRecord.points;
    }

    public bool NewTimeRecord(float time)
    {
        return currentRecord == null || time > currentRecord.time;
    }

    public void UpdateRecords(Record record)
    {
        newPointsRecord = false;
        newTimeRecord = false;

        if (currentRecord != null)
        {
            if (NewPointsRecord(record.points))
            {
                currentRecord.points = record.points;
                newPointsRecord = true;
            }

            if (NewTimeRecord(record.time))
            {
                currentRecord.time = record.time;
                newTimeRecord = true;
            }
        }
        else
        {
            currentRecord = record;
            newPointsRecord = true;
            newTimeRecord = true;
        }

        Save();
    }

    public static GameRecords LoadRecords(string name = "Records.txt")
    {
        string path = Application.persistentDataPath + "/" + name;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameRecords records = formatter.Deserialize(stream) as GameRecords;
            stream.Close();
            return records;
        }

        return new GameRecords();
    }

    public void Save(string name = "Records.txt")
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, this);
        stream.Close();
    }
}