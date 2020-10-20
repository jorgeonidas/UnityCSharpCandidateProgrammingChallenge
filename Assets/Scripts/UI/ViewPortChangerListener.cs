using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ViewPortChangerListener : MonoBehaviour
    {
        RectTransform rectTransform;
        GridLayoutGroup gridLayoutGroup;
        float maxWidth;
        float originalCellZise;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
            originalCellZise = gridLayoutGroup.cellSize.x;
            maxWidth = rectTransform.rect.width;
        }

        private void OnEnable()
        {
            DataDisplayer.updateViewportAction = RecalculateViewPortPosition;
        }

        private void OnDisable()
        {
            DataDisplayer.updateViewportAction -= RecalculateViewPortPosition;
        }

        public void RecalculateViewPortPosition(int columCount)
        {
            var cellWidth = originalCellZise;
            var widthToCheck = cellWidth * columCount;
            gridLayoutGroup.cellSize = new Vector2(cellWidth, gridLayoutGroup.cellSize.y);
            if (widthToCheck > maxWidth)
            {
                cellWidth = (maxWidth / columCount);
                gridLayoutGroup.cellSize = new Vector2(cellWidth, gridLayoutGroup.cellSize.y);
            }
            var newXPos = maxWidth / 2;
            rectTransform.sizeDelta = new Vector2(maxWidth, rectTransform.rect.height);
            rectTransform.anchoredPosition = new Vector2(-newXPos, rectTransform.anchoredPosition.y);

        }
    }
}


