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
        InputField inputField;
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
            inputField = GetComponentInChildren<InputField>();
            inputField.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            DataDisplayer.toggleEditModeAction += ToggleEditMode;
        }

        private void OnDisable()
        {
            DataDisplayer.toggleEditModeAction -= ToggleEditMode;
        }

        public void FillCell(string value, bool header = false)
        {
            Debug.Log(value + " "+ header);
            this.isHeader = header;
            
            dataText.fontSize = this.isHeader ? fontSizeHeader : fontSizeRegular;
            if (this.isHeader)
                dataText.fontStyle = FontStyle.Bold;

            this.cellValue = value;
            dataText.text = this.cellValue;

        }

        private void ToggleEditMode(bool editMode)
        {
            if (!this.isHeader)
            {
                dataText.gameObject.SetActive(!editMode);
                inputField.gameObject.SetActive(editMode);
                if (editMode)
                    inputField.text = this.cellValue;
            }
        }
    }
}


