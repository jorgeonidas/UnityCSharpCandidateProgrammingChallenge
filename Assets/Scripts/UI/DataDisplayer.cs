using Models;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI
{
    public class DataDisplayer : MonoBehaviour
    {
        [Header("File Relative Path")]
        public string fileRelativePath = "JsonChallenge.json";
        [Header("UI elements")]
        public Text tableTitleText;
        public GridLayoutGroup gridLayoutGroup;

        TeamMembersModel teamMembers;
        [Header("CellElement")]
        public TableCell cellItem;
        int columCount;

        void Start()
        {
            teamMembers = FileManager.LoadModelFromJsonFile(fileRelativePath);
            if (teamMembers != null)
                Fill(teamMembers);
            else
                Debug.LogError("Error loading data");
        }

        private void OnEnable()
        {
            TableCell.cellEditedAction += UpdateCell;
        }

        private void OnDisable()
        {
            TableCell.cellEditedAction -= UpdateCell;
        }

        public void Fill(TeamMembersModel teamMembersModel)
        {
            ClearCells();
            var headers = teamMembersModel.ColumnHeaders;
            var data = teamMembersModel.Data;
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = headers.Count;
            //title
            tableTitleText.text = teamMembersModel.Title;
            //fill headers
            FillHeaders(headers);
            FillData(data);
        }

        public void ClearCells()
        {
            foreach (Transform child in gridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void FillHeaders(List<string> headers)
        {
            columCount = headers.Count();
            for (int i = 0; i < columCount; i++)
            {
                var header = Instantiate(cellItem, gridLayoutGroup.transform);
                header.FillCell(headers[i], true);
            }
        }

        public void FillData(List<TeamMemberModel> data)
        {
            for(int row = 0; row < data.Count(); row++)
            {
                var memberFields = data[row].GetType().GetFields();
                for (int colum = 0; colum < columCount; colum++)
                {
                    var cell = Instantiate(cellItem, gridLayoutGroup.transform);
                    if(colum < memberFields.Count())
                    {
                        cell.FillCell((string)memberFields.ElementAt(colum).GetValue(data[row]));
                        cell.SetCellCoord(row, colum);
                    }
                    else
                    {
                        cell.FillCell("<color=red>No Exist</color>");
                    }
                }
            }
        }

        public void AddAction()
        {

        }

        public void DeleteAction()
        {

        }

        public void UpdateCell(Vector2 cellCoord, string newValue)
        {
            if(newValue != "")
            {
                Debug.Log("cell to modify " + cellCoord + " " + newValue);
                int dataIndexToModify = (int)cellCoord.x;
                var teamMemberToModify = teamMembers.Data[dataIndexToModify];
                Debug.Log(teamMemberToModify.ID + " " + teamMemberToModify.Name + " " + teamMemberToModify.Role + " " + teamMemberToModify.Nickname);
            }
        }

    }
}


