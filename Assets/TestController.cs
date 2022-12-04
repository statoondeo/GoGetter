using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    protected GoGetterBoard Game;

	protected void Awake() => Game = new GoGetterBoard();

	protected void Start()
	{
		for (int i = 0; i < Game.Slots.Count; i++) Game.FillSlot(i, i);

		//// Check des requirements
		//StringBuilder results = new();
		//IList<int> itemIds = GoGetterData.GetItemIds();
		//for (int i = 0; i < itemIds.Count - 1; i++)
		//	for (int j = i + 1; j < itemIds.Count; j++)
		//		if (Game.CheckRequirement(itemIds[i], itemIds[j])) results.AppendLine($"Chemin({itemIds[i]}, {itemIds[j]})=OK");
		//Debug.Log(results.ToString());

		// Check des impasses
		IList<int> deadEndIdsList = Game.CheckPathes();
		Debug.Log($"Check DeadEnd={deadEndIdsList.Count}");
	}
}
