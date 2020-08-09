using Models;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using FileManagment;

namespace UI
{
    public class DataDisplayer : MonoBehaviour
    {
        public static Action<float, int> updateViewportAction;

        [Header("File Relative Path")]
        public string fileRelativePath = "JsonChallenge.json";
        [Header("UI elements")]
        public Text tableTitleText;
        public GridLayoutGroup gridLayoutGroup;

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
            FileWatcher.fileChangedAction += SetFileChangedFlag;
        }

        private void OnDisable()
        {
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
            foreach (Transform child in gridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void FillHeaders(List<string> headers)
        {
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = headers.Count;
            columCount = gridLayoutGroup.constraintCount;

            if (updateViewportAction != null)
                updateViewportAction(gridLayoutGroup.cellSize.x, columCount);

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

        public void SetFileChangedFlag()
        {
            fileChangedFlag = true;
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}


