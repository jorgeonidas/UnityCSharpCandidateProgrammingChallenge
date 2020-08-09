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
        //public static Action<bool> toggleEditModeAction;

        [Header("File Relative Path")]
        public string fileRelativePath = "JsonChallenge.json";
        [Header("UI elements")]
        public Text tableTitleText;
        public GridLayoutGroup gridLayoutGroup;
       // public Toggle modifyToggle;
        TeamMembersModel teamMembers;
        [Header("CellElement")]
        public TableCell cellItem;
        // Start is called before the first frame update
        int columCount;
        void Start()
        {
            teamMembers = FileManager.LoadModelFromJsonFile(fileRelativePath);
            if (teamMembers != null)
                Fill(teamMembers);
            else
                Debug.LogError("Error loading data");

            //modifyToggle.isOn = false;
            //modifyToggle.onValueChanged.AddListener(ToggleEditMode);
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

        private void ToggleEditMode(bool edit)
        {
            Debug.Log(edit);
            //if (toggleEditModeAction != null)
            //    toggleEditModeAction(edit);

            //if (!edit)
            //{
            //    foreach (Transform child in gridLayoutGroup.transform)
            //    {
            //        if (child.GetComponent<TableCell>())
            //        {
            //            var cell = child.GetComponent<TableCell>();
            //            if (!cell.IsHeader())
            //            {
            //                Debug.Log(cell.GetValue());
            //            }
            //        }
            //    }
            //}
        }
    }
}


