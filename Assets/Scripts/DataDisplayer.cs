using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDisplayer : MonoBehaviour
{
    [Header("File Relative Path")]
    public string fileRelativePath = "JsonChallenge.json";
    TeamMembersModel teamMembers;
    // Start is called before the first frame update
    void Start()
    {
        teamMembers = FileManager.LoadModelFromJsonFile(fileRelativePath);
        if(teamMembers != null)
            Debug.Log(teamMembers.Data[0].ID + " " + teamMembers.Data[0].Name);
        else
            Debug.LogError("Error loading data");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
