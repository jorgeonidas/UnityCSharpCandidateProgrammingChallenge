using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileWatcher : MonoBehaviour
{
    public static Action fileChangedAction;
    // Start is called before the first frame update
    void Start()
    {
        CreateFileWatcher(Application.streamingAssetsPath);
    }

    public void CreateFileWatcher(string path)
    {

        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = path;

        watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;

        watcher.Filter = "*.json";

        watcher.Changed += new FileSystemEventHandler(OnChanged);

        watcher.EnableRaisingEvents = true;
    }

    private static void OnChanged(object source, FileSystemEventArgs e)
    {
        Debug.Log("File: " + e.FullPath + " " + e.ChangeType);
        if (fileChangedAction != null)
            fileChangedAction();
    }
}
