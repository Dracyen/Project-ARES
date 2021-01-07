using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    
    public static void SaveTrack(MapDisplay track,string fileName, MapGenerator TrackInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName + ".fun";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        Save SavedTracks = new Save(track, TrackInfo);

        formatter.Serialize(stream, SavedTracks);
        stream.Close();
    }

    public static Save LoadTracks(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".fun";
        Debug.Log(path);
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
       
    }
    public static List<string> LoadTrackNames()
    {
        DirectoryInfo d = new DirectoryInfo(Application.persistentDataPath);//Assuming Test is your Folder
        FileInfo[] Files = d.GetFiles("*.fun"); //Getting Text files
        List<string> str = new List<string>();
        string pre;
        int i = 0;
        foreach (FileInfo file in Files)
        {
            pre = file.Name;
            pre = pre.Remove(pre.Length - 4);
            str.Add(pre);
            
           
            i++;
        }
        return str;
    }
    public static void RenameTrack(string fileName, string newName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".fun";
        string newPath = Application.persistentDataPath + "/" + newName + ".fun";
        System.IO.File.Move(path, newPath);
    }
    public static void DeleteTrack(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".fun";
        System.IO.File.Delete(path);
    }

}
