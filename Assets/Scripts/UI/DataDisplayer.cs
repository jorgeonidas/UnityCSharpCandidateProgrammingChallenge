﻿using Models;
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
            foreach(var member in data)
            {
                var memberFields = member.GetType().GetFields();
                for (int colum = 0; colum < columCount; colum++)
                {
                    var cell = Instantiate(cellItem, gridLayoutGroup.transform);
                    if(colum < memberFields.Count())
                    {
                        cell.FillCell((string)memberFields.ElementAt(colum).GetValue(member));
                    }
                    else
                    {
                        cell.FillCell("No Exist!");
                    }
                }
            }
        }
    }
}

