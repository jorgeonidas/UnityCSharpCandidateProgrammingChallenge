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
    public GridLayoutGroup gridLayoutGroup;
    TeamMembersModel teamMembers;
    [Header("CellElement")]
    public GameObject cellItem;
    // Start is called before the first frame update
    void Start()
    {
        teamMembers = FileManager.LoadModelFromJsonFile(fileRelativePath);
        if (teamMembers != null)
            Fill(teamMembers);
        else
            Debug.LogError("Error loading data");
    }
    
    public void Fill(TeamMembersModel teamMembersModel)
    {
        ClearCells();
        var headers = teamMembersModel.ColumnHeaders;
        var data = teamMembersModel.Data;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = headers.Count;

        //fill headers
        for(int i = 0; i < headers.Count; i++)
        {
            Instantiate(cellItem, gridLayoutGroup.gameObject.transform);  
        }
    }

    public void ClearCells()
    {
        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
