using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ViewPortChangerListener : MonoBehaviour
    {
        private void OnEnable()
        {
            DataDisplayer.updateViewportAction = RecalculateViewPortPosition;
        }

        private void OnDisable()
        {
            DataDisplayer.updateViewportAction -= RecalculateViewPortPosition;
        }

        public void RecalculateViewPortPosition(float cellWidth, int columCount)
        {
            var rectTransform = GetComponent<RectTransform>();

            var width = cellWidth * columCount;
            var newXPos = width / 2;
            rectTransform.sizeDelta = new Vector2(width, rectTransform.rect.height);
            rectTransform.anchoredPosition = new Vector2(-newXPos, rectTransform.anchoredPosition.y);
            
        }
    }
}


