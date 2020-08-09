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
        public Transform gridTransform;

        TeamMembersModel teamMembers;
        [Header("CellElement")]
        public TableCell cellItem;
        int columCount;
        bool fileChangedFlag = false;
        void Start()
        {
            LoadFromFile();
        }

        private void OnEnable()
        {
            TableCell.cellEditedAction += UpdateCell;
            FileWatcher.fileChangedAction += SetFileChangedFlag;
        }

        private void OnDisable()
        {
            TableCell.cellEditedAction -= UpdateCell;
            FileWatcher.fileChangedAction -= SetFileChangedFlag;
        }

        private void Update()
        {
            if (fileChangedFlag)
            {
                fileChangedFlag = false;
                LoadFromFile();
            }
        }

        public void LoadFromFile()
        { 
            teamMembers = FileManager.LoadModelFromJsonFile(fileRelativePath);
            if (teamMembers != null)
                Fill(teamMembers);
            else
                Debug.LogError("Error loading data");
        }

        public void Fill(TeamMembersModel teamMembersModel)
        {
            try { 
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
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }

        public void ClearCells()
        {
            foreach (Transform child in gridTransform)
            {
                Destroy(child.gameObject);
            }
        }

        public void FillHeaders(List<string> headers)
        {
            columCount = headers.Count();
            for (int i = 0; i < columCount; i++)
            {
                var header = Instantiate(cellItem, gridTransform);
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
                    var cell = Instantiate(cellItem, gridTransform);
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
                int dataIndexToModify = (int)cellCoord.x;
                int fieldIndexToModify = (int)cellCoord.y;

                var teamMemberToModify = teamMembers.Data[dataIndexToModify];
                var memberFields = teamMemberToModify.GetType().GetFields();
                memberFields.ElementAt(fieldIndexToModify).SetValue(teamMemberToModify, newValue);
                string updatedData = JsonUtility.ToJson(teamMembers, true);

                FileManager.Write(fileRelativePath, updatedData);
            }
        }

        public void SetFileChangedFlag()
        {
            fileChangedFlag = true;
        }

    }
}


