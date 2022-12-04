﻿using System.Collections.Generic;
using System.Text;
using Debug = UnityEngine.Debug;

/// <summary>
/// Gestion du plateau de jeu
/// </summary>
public sealed class GoGetterBoard
{
	/// <summary>
	/// Liste des emplacement du plateau de jeu
	/// </summary>
	public IList<GoGetterSlot> Slots { get; private set; }
	/// <summary>
	/// Liste des tuiles à utiliser
	/// </summary>
	public IList<GoGetterTile> Tiles { get; private set; }

	private readonly GoGetterGraph Graph;
	private readonly GoGetterGraphData NoPathGraphData;
	private readonly GoGetterGraphConstraint GraphConstraint;
	private readonly IDictionary<int, int> SlotTileBindings;

	public GoGetterBoard()
	{
		Graph = new GoGetterGraph(new GoGetterVertexFactory(), new GoGetterEdgeFactory());
		Slots = GoGetterData.GetSlots();
		Tiles = GoGetterData.GetTiles();
		NoPathGraphData = new GoGetterGraphData(-1);
		GraphConstraint = new GoGetterGraphConstraint();
		SlotTileBindings = new Dictionary<int, int>();
	}
	/// <summary>
	/// Contrôle que chaque slot contient une tuile
	/// </summary>
	/// <returns>true si le plateau de jeu est complet, false sinon</returns>
	public bool CheckBoard() => SlotTileBindings.Count == Slots.Count;
	/// <summary>
	/// Contrôle qu'aucun chemin n'est interrompu.
	/// Un chemin interrompu est un chemin qui ne mène pas à un item ou une tuile voisine.
	/// </summary>
	/// <returns>true si aucun chemin n'est interrompu, alse sinon</returns>
	public IList<int> CheckPathes()
	{
		IList<int> deadEnds = new List<int>();
		IList<int> connexionIdsList = GoGetterData.GetConnexionsIds();

		for (int i = 0; i < connexionIdsList.Count; i++)
		{
			HashSet<GoGetterSlot> slots = new();
			GoGetterVertex vertex = Graph.GetVertex(connexionIdsList[i]) as GoGetterVertex;
			for (int j = 0; j < vertex.Edges.Count; j++)
				for (int k = 0; k < Slots.Count; k++)
				{
					if (Slots[k].ContainsVertices(vertex.Id, vertex.Edges[j].Target.Id)) slots.Add(Slots[k]);
				}
			if ((slots.Count == 1) && !deadEnds.Contains(vertex.Id)) deadEnds.Add(vertex.Id);
		}
		return (deadEnds);
	}
	/// <summary>
	/// Contrôle si il existe un chemin entre 2 items
	/// </summary>
	/// <param name="origin">Id de l'item d'origine</param>
	/// <param name="target">Id de l'item destination</param>
	/// <returns>true si il y a un chemin entre l'origine et la destination, false sinon</returns>
	public bool CheckRequirement(int origin, int target) => Graph.FindPath(origin, target, NoPathGraphData, GraphConstraint) != NoPathGraphData;
	/// <summary>
	/// Associe une tuile à un slot sur la plateau de jeu
	/// </summary>
	/// <param name="slotIndex">index du slot</param>
	/// <param name="tileIndex">index de la tuile</param>
	public void FillSlot(int slotIndex, int tileIndex)
	{
		GoGetterTile tile = Tiles[tileIndex];
		for (int i = 0; i < tile.EdgeCount; i++)
		{
			(int, int) edge = tile.GetEdge(i);
			GoGetterSlot slot = Slots[slotIndex];
			Graph.AddEdge(slot.GetVertexId(edge.Item1), slot.GetVertexId(edge.Item2), new GoGetterGraphData(1));
		}
		SlotTileBindings.Add(slotIndex, tileIndex);
	}
	/// <summary>
	/// Vide le slot demandé
	/// </summary>
	/// <param name="slotIndex">index du slot</param>
	public void EmptySlot(int slotIndex)
	{
		if (!SlotTileBindings.ContainsKey(slotIndex)) return;
		GoGetterSlot slot = Slots[slotIndex];
		GoGetterTile tile = Tiles[SlotTileBindings[slotIndex]];
		SlotTileBindings.Remove(slotIndex);
		for (int i = 0; i < tile.EdgeCount; i++)
		{
			(int, int) edge = tile.GetEdge(i);
			Graph.RemoveEdge(slot.GetVertexId(edge.Item1), slot.GetVertexId(edge.Item2));
		}
	}
}