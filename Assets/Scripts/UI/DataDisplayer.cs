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
        // Start is called before the first frame update
        int columCount;
        int rowCount;
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
                var header = Instantiate(cellItem, gridLayoutGroup.gameObject.transform);
                header.FillCell(headers[i], true);
            }
        }

        public void FillData(List<TeamMemberModel> data)
        {
            var type = typeof(TeamMemberModel);
            var fieldsCount = type.GetFields().Count();
            rowCount = data.Count();
            var fields = type.GetFields();
           /* for(int i = 0; i < rowCount; i++)
            {
                for(int j = 0; j < fieldsCount; j++)
                {
                    var cell = Instantiate(cellItem, gridLayoutGroup.transform);
                }
            }*/

            foreach (var member in data)
            {
                Type objType = member.GetType();

                foreach (var field in objType.GetFields().Where(field => field.IsPublic))
                {
                    var cell = Instantiate(cellItem, gridLayoutGroup.transform);
                   // Debug.Log(string.Format("Name: {0} Value: {1}", field.Name, field.GetValue(member)));
                    cell.FillCell((string)field.GetValue(member));
                }
            }
        }
    }
}


