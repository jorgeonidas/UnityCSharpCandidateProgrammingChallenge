using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.IO;

public class FileManager
{
    const string tailingRegEx = "\\,(?!\\s*?[\\{\\[\"\'\\w])";
    // Start is called before the first frame update

    public static TeamMembersModel LoadModelFromJsonFile(string fileName)
    {
        TeamMembersModel teamMembersModel = null;
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string result;
        if (File.Exists(filePath))
        {
            result = File.ReadAllText(filePath);
            result = TrailingCheck(result);
            teamMembersModel = JsonUtility.FromJson<TeamMembersModel>(result);
        }
        else
        {
            Debug.LogError(fileName +" does not exists or incorrect path");
        }

        return teamMembersModel;
    }

    public static string TrailingCheck(string jsonString)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(tailingRegEx);
        return regex.Replace(jsonString, "");
    }

    //public void Write(string fileName)
    //{
    //    string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

    //    StreamWriter writer = new StreamWriter(filePath, true);
    //    writer.WriteLine("Test");
    //    writer.Close();

    //    LoadJson(fileName);
    //}
}
