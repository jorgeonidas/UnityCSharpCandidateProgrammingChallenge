using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace UI
{
    public class TableCell : MonoBehaviour
    {
        Text dataText;
        [SerializeField]
        int fontSizeHeader = 60;
        [SerializeField]
        int fontSizeRegular = 56;
        bool isHeader = false;
        string cellValue;
        // Start is called before the first frame update
        void Awake()
        {
            dataText = GetComponentInChildren<Text>();
        }

        public void FillCell(string value, bool header = false)
        {
            this.isHeader = header;

            dataText.fontSize = this.isHeader ? fontSizeHeader : fontSizeRegular;
            if (this.isHeader)
                dataText.fontStyle = FontStyle.Bold;

            this.cellValue = (value != null && value != "") ? value : "<color=red>Empty</color>";
            dataText.text = this.cellValue;

        }
    }
}


