using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    public class TableCell : MonoBehaviour
    {
        Text dataText;
        [SerializeField]
        int fontZiseHeader = 60;
        // Start is called before the first frame update
        void Awake()
        {
            dataText = GetComponentInChildren<Text>();
        }

        public void FillCell(string value, bool header = false)
        {
            if (header)
            {
                dataText.fontSize = fontZiseHeader;
                dataText.fontStyle = FontStyle.Bold;
            }

            dataText.text = value;
        }
    }
}


