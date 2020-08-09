using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPortChangerListener : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnRectTransformDimensionsChange()
    {
        RecalculateViewPortPosition();
    }

    public void RecalculateViewPortPosition()
    {
        var widht = rectTransform.rect.width;
        Debug.Log(widht);
        var newXPos = widht / 2;
        rectTransform.anchoredPosition = new Vector2(-newXPos, rectTransform.anchoredPosition.y);
    }
}
