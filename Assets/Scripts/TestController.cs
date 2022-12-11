using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TestController : MonoBehaviour
{
	[SerializeField] protected ScriptableTestGrid TestGrid;

    protected GoGetterBoard Board;
	protected void Awake() => Board = new GoGetterBoard();
	protected void Start()
	{
		if (null == TestGrid) throw new ArgumentNullException(nameof(TestGrid));

		// Consitution de la grille
		for (int i = 0; i < TestGrid.TilesPositionAndRotation.Length; i++)
		{
			int tileIndex = TestGrid.TilesPositionAndRotation[i].x;
			for (int j = 0; j < TestGrid.TilesPositionAndRotation[i].y; j++) Board.Tiles[tileIndex].Rotate();
			Board.FillSlot(i, tileIndex);
		}

		// Check des impasses
		Debug.Log($"DeadEnd={ListToString(Board.CheckPathes())}");

		// Check des requirements
		StringBuilder results = new();
		IList<int> itemIds = GoGetterData.GetItemIds();
		for (int i = 0; i < itemIds.Count - 1; i++)
			for (int j = i + 1; j < itemIds.Count; j++)
			{
				ISet<int> path = Board.CheckRequirement(itemIds[i], itemIds[j]);
				if (null != path)  results.AppendLine($"Path({itemIds[i]}, {itemIds[j]})={ListToString(path)}");
			}
		Debug.Log(results.ToString());
	}
	protected static string ListToString(ISet<int> path)
	{
		StringBuilder sb = new();
		foreach(int vertexId in path) sb.Append($"\t {vertexId}");
		return (sb.ToString());
	}
}
