using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileWatcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // string filePath = Path.Combine(Application.streamingAssetsPath, "JsonChallenge.json");
        CreateFileWatcher(Application.streamingAssetsPath);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CreateFileWatcher(string path)
    {

        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = path;

        watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;

        watcher.Filter = "*.json";

        watcher.Changed += new FileSystemEventHandler(OnChanged);
        watcher.Created += new FileSystemEventHandler(OnChanged);
        watcher.Deleted += new FileSystemEventHandler(OnChanged);
        watcher.Renamed += new RenamedEventHandler(OnRenamed);

        watcher.EnableRaisingEvents = true;
    }

    private static void OnChanged(object source, FileSystemEventArgs e)
    {
        Debug.Log("File: " + e.FullPath + " " + e.ChangeType);
    }

    private static void OnRenamed(object source, RenamedEventArgs e)
    {
        Debug.LogFormat("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
    }
}
