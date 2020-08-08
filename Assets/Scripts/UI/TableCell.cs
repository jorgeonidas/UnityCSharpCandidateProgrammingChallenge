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
        int fontSizeHeader = 60;
        [SerializeField]
        int fontSizeRegular = 56;
        // Start is called before the first frame update
        void Awake()
        {
            dataText = GetComponentInChildren<Text>();
        }

        public void FillCell(string value, bool header = false)
        {
            dataText.fontSize = header ? fontSizeHeader : fontSizeRegular;
            if (header)
                dataText.fontStyle = FontStyle.Bold;

            dataText.text = value;
        }
    }
}


