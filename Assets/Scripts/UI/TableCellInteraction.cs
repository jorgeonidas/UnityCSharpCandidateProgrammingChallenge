using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableCellInteraction : MonoBehaviour, IPointerClickHandler
{
    public delegate void DoubleClickDelegate();
    public DoubleClickDelegate doubleClickDelegate;
    float lastClick = 0f;
    float interval = 0.4f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((lastClick + interval) > Time.time)
        {
            Debug.Log("Double click");
            if (doubleClickDelegate != null)
                doubleClickDelegate();
        }
        lastClick = Time.time;
    }
}
