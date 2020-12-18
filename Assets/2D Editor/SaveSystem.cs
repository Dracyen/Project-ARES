using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveTrack(MapDisplay track,string fileName, MapGenerator TrackInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        Save SavedTracks = new Save(track, TrackInfo);

        formatter.Serialize(stream, SavedTracks);
        stream.Close();
    }

    public static Save LoadTracks(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Save data = formatter.Deserialize(stream) as Save;
            stream.Close();


            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }

    public static void SaveTracksNames(string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SavedTracks";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        TracksNamesData SavedTracksnames = new TracksNamesData(name);

        formatter.Serialize(stream, SavedTracksnames);
        stream.Close();
    }
    public static TracksNamesData LoadTrackNames()
    {
        string path = Application.persistentDataPath + "/SavedTracks";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TracksNamesData data = formatter.Deserialize(stream) as TracksNamesData;
            stream.Close();


            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}
