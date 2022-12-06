using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TestController : MonoBehaviour
{
    protected GoGetterBoard Game;
	protected void Awake() => Game = new GoGetterBoard();
	protected void Start()
	{
		for (int i = 0; i < Game.Slots.Count; i++) Game.FillSlot(i, i);

		// Check des requirements
		StringBuilder results = new();
		IList<int> itemIds = GoGetterData.GetItemIds();
		for (int i = 0; i < itemIds.Count - 1; i++)
			for (int j = i + 1; j < itemIds.Count; j++)
			{
				IList<int> path = Game.CheckRequirement(itemIds[i], itemIds[j]);
				if (null != path)  results.AppendLine($"Path({itemIds[i]}, {itemIds[j]})={ListToString(path)}");
			}
		Debug.Log(results.ToString());

		// Check des impasses
		Debug.Log($"DeadEnd={ListToString(Game.CheckPathes())}");
	}
	protected string ListToString(IList<int> path)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < path.Count; i++) sb.Append($"\t {path[i]}");
		return (sb.ToString());
	}
}
