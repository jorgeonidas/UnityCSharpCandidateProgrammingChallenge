﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.IO;
using System.Text.RegularExpressions;

namespace FileManagment
{

    public class FileManager
    {
        const string tailingRegEx = "\\,(?!\\s*?[\\{\\[\"\'\\w])";

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
                Debug.LogError(fileName + " does not exists or incorrect path");
            }

            return teamMembersModel;
        }

        public static string TrailingCheck(string jsonString)
        {
            Regex regex = new Regex(tailingRegEx);
            return regex.Replace(jsonString, "");
        }
    }
}
