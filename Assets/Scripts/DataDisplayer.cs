using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplayer : MonoBehaviour
{
    [Header("File Relative Path")]
    public string fileRelativePath = "JsonChallenge.json";
    [Header("UI elements")]
    public Text tableTitleText;
    TeamMembersModel teamMembers;
    // Start is called before the first frame update
    void Start()
    {
        teamMembers = FileManager.LoadModelFromJsonFile(fileRelativePath);
        if (teamMembers != null)
        {

        }
        else
            Debug.LogError("Error loading data");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
