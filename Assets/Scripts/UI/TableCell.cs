using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace UI
{
    public class TableCell : MonoBehaviour
    {
        public static Action<Vector2, string> cellEditedAction;

        Text dataText;
        InputField inputField;
        [SerializeField]
        int fontSizeHeader = 60;
        [SerializeField]
        int fontSizeRegular = 56;
        TableCellInteraction tableCellInteraction;
        bool isHeader = false;
        string cellValue;
        Vector2 cellCoord = new Vector2(-1, -1);
        // Start is called before the first frame update
        void Awake()
        {
            dataText = GetComponentInChildren<Text>();
            inputField = GetComponentInChildren<InputField>();
            inputField.gameObject.SetActive(false);
            tableCellInteraction = GetComponent<TableCellInteraction>();
        }

        private void OnEnable()
        {
            tableCellInteraction.doubleClickDelegate = ToggleEdit;
        }

        private void OnDisable()
        {
            tableCellInteraction.doubleClickDelegate -= ToggleEdit;
        }

        private void OnGUI()
        {
            if (inputField.isFocused && (Input.GetKey(KeyCode.Return)||(Input.GetKey(KeyCode.KeypadEnter))))
            {
                var inEditMode = inputField.gameObject.activeSelf;
                if (inEditMode)
                {
                    if (inputField.text == "")
                        inputField.text = this.cellValue;
                    ToggleEditMode(!inEditMode);
                }
            }
        }

        public void FillCell(string value, bool header = false)
        {
            this.isHeader = header;

            dataText.fontSize = this.isHeader ? fontSizeHeader : fontSizeRegular;
            if (this.isHeader)
                dataText.fontStyle = FontStyle.Bold;

            this.cellValue = value;
            dataText.text = this.cellValue;

        }

        public void SetCellCoord(int rowPos, int colPos)
        {
            this.cellCoord = new Vector2(rowPos, colPos);
        }

        private void ToggleEdit()
        {
            if (!isHeader)
            {
                Debug.Log(cellCoord);
                var inEditMode = inputField.gameObject.activeSelf;
                ToggleEditMode(!inEditMode);
            }
        }

        private void ToggleEditMode(bool editMode)
        {
            if (!this.isHeader)
            {
                dataText.gameObject.SetActive(!editMode);
                inputField.gameObject.SetActive(editMode);
                if (editMode)
                {
                    inputField.text = this.cellValue;
                    inputField.Select();
                    inputField.ActivateInputField();
                }
                else
                {
                    if (inputField.text != this.cellValue)
                    {
                        this.cellValue = inputField.text;
                        dataText.text = this.cellValue;

                        if (cellEditedAction != null)
                            cellEditedAction(cellCoord, this.cellValue);
                    }
                }

            }
        }

        public bool IsHeader()
        {
            return isHeader;
        }

        public string GetValue()
        {
            return this.cellValue;
        }
    }
}


