using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.IO;

public class FileManager : MonoBehaviour
{
    [Header("File Relative Path")]
    public string fileRelativePath = "JsonChallenge.json";

    string tarilingRegEx = "\\,(?!\\s*?[\\{\\[\"\'\\w])";
    // Start is called before the first frame update
    void Start()
    {
        LoadJson(fileRelativePath);
        //Write("JsonChallenge.json");
    }

    public void LoadJson(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        string result;
        if (File.Exists(filePath))
        {
            result = File.ReadAllText(filePath);
            Debug.Log(result);

            result = TrailingCheck(result);

            Debug.Log(result);

            TeamMembersModel teamMembersModel = JsonUtility.FromJson<TeamMembersModel>(result);
        }
        else
        {
            Debug.LogError(fileName +" does not exists or incorrect path");
        }
    }

    public string TrailingCheck(string jsonString)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(tarilingRegEx);
        return regex.Replace(jsonString, "");
    }

    //public void Write(string fileName)
    //{
    //    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

    //    StreamWriter writer = new StreamWriter(filePath, true);
    //    writer.WriteLine("Test");
    //    writer.Close();

    //    LoadJson(fileName);
    //}
}
