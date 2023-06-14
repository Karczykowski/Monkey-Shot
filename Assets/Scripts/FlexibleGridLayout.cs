using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
	public enum FitType
	{
		Uniform,
		Width,
		Height,
		FixedRows,
		FixedColumns
	}

	public FitType fitType;
	public int rows;
	public int columns;
	public Vector2 cellSize;
	public Vector2 spacing;

	public bool fitX;
	public bool fitY;

	public bool squareCell;
	public bool showOnlyVisibleChilds;

	public float minPanelHeight;
	public bool adjustHeight;

	private int visibleChilds;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();

		visibleChilds = transform.childCount;

		if (showOnlyVisibleChilds)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				if (!transform.GetChild(i).gameObject.activeSelf)
					visibleChilds--;
			}
		}

		if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
		{
			fitX = true;
			fitY = true;

			float sqrRt = Mathf.Sqrt(visibleChilds);
			rows = Mathf.CeilToInt(sqrRt);
			columns = Mathf.CeilToInt(sqrRt);
		}

		if (fitType == FitType.Width || fitType == FitType.FixedColumns)
		{
			rows = Mathf.CeilToInt(visibleChilds / (float)columns);
		}

		if (fitType == FitType.Height || fitType == FitType.FixedRows)
		{
			columns = Mathf.CeilToInt(visibleChilds / (float)rows);
		}

		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		float cellWidth = (parentWidth - (spacing.x * (columns - 1)) - padding.left - padding.right) / columns;
		float cellHeight = (parentHeight - (spacing.y * (rows - 1)) - padding.top - padding.bottom) / rows;

		cellSize.x = fitX ? cellWidth : cellSize.x;
		cellSize.y = squareCell ? cellSize.x : (fitY ? cellHeight : cellSize.y);

		int columnCount = 0;
		int rowCount = 0;

		for (int i = 0; i < rectChildren.Count; i++)
		{
			rowCount = i / columns;

			columnCount = i % columns;

			var item = rectChildren[i];

			var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
			var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

			SetChildAlongAxis(item, 0, xPos, cellSize.x);
			SetChildAlongAxis(item, 1, yPos, cellSize.y);
		}

		if (adjustHeight)
		{
			int correctRows = visibleChilds / columns;
			if (visibleChilds % columns != 0)
				correctRows++;

			RectTransform rectTransform = GetComponent<RectTransform>();
			float newHeight = correctRows * cellSize.y + padding.top + padding.bottom + (correctRows - 1) * spacing.y;
			newHeight = Mathf.Clamp(newHeight, minPanelHeight, 100000);

			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
		}
	}

	public override void CalculateLayoutInputVertical()
	{

	}

	public override void SetLayoutHorizontal()
	{

	}

	public override void SetLayoutVertical()
	{

	}
}