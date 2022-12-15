using System.Collections.Generic;

/// <summary>
/// Gestion du plateau de jeu
/// </summary>
public sealed class GoGetterBoard
{
	/// <summary>
	/// Liste des emplacement du plateau de jeu
	/// </summary>
	public IList<GoGetterSlot> Slots { get; }

	/// <summary>
	/// Liste des tuiles à utiliser
	/// </summary>
	public IList<GoGetterTile> Tiles { get; }

	private readonly Graph Graph;
	private readonly TwoDictionary<int, int> SlotTileBindings;

	public GoGetterBoard()
	{
		Graph = new GoGetterGraph();
		Slots = GoGetterData.GetSlots();
		Tiles = GoGetterData.GetTiles();
		SlotTileBindings = new TwoDictionary<int, int>();
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
	public ISet<int> CheckPathes()
	{
		ISet<int> deadEnds = new HashSet<int>();
		IList<int> connexionIdsList = GoGetterData.GetConnexionsIds();

		for (int i = 0; i < connexionIdsList.Count; i++)
		{
			ISet<GoGetterSlot> slots = new HashSet<GoGetterSlot>();
			Vertex vertex = Graph.GetVertex(connexionIdsList[i]) as Vertex;
			foreach (Edge edge in vertex.Edges)
				for (int k = 0; k < Slots.Count; k++)
					if (Slots[k].ContainsVertices(vertex.Id, edge.Target)) slots.Add(Slots[k]);
			if (slots.Count == 1) deadEnds.Add(vertex.Id);
		}
		return (deadEnds);
	}

	/// <summary>
	/// Contrôle si il existe un chemin entre 2 items
	/// </summary>
	/// <param name="origin">Id de l'item d'origine</param>
	/// <param name="target">Id de l'item destination</param>
	/// <returns>true si il y a un chemin entre l'origine et la destination, false sinon</returns>
	public ISet<int> CheckRequirement(int origin, int target) => Graph.FindPath(origin, target);

	/// <summary>
	/// Associe une tuile à un slot sur la plateau de jeu
	/// </summary>
	/// <param name="slotIndex">index du slot</param>
	/// <param name="tileIndex">index de la tuile</param>
	private void FillSlot(int slotIndex, int tileIndex)
	{
		GoGetterTile tile = Tiles[tileIndex];
		for (int i = 0; i < tile.EdgeCount; i++)
		{
			(int origin, int target) = tile.GetEdge(i);
			GoGetterSlot slot = Slots[slotIndex];
			Graph.AddEdge(slot.GetVertexId(origin), slot.GetVertexId(target), new GraphData(1));
		}
		SlotTileBindings.Add(slotIndex, tileIndex);
	}

	/// <summary>
	/// Vide le slot demandé
	/// </summary>
	/// <param name="slotIndex">index du slot</param>
	private void EmptySlot(int slotIndex)
	{
		if (!SlotTileBindings.ContainsForwardKey(slotIndex)) return;
		GoGetterSlot slot = Slots[slotIndex];
		GoGetterTile tile = Tiles[SlotTileBindings.GetForwardValue(slotIndex)];
		SlotTileBindings.RemoveForward(slotIndex);
		for (int i = 0; i < tile.EdgeCount; i++)
		{
			(int, int) edge = tile.GetEdge(i);
			Graph.RemoveEdge(slot.GetVertexId(edge.Item1), slot.GetVertexId(edge.Item2));
		}
	}

	public void SwitchTiles(int slotIndex, int tileIndex, int newSlotIndex, int newTileIndex)
	{
		if (tileIndex == -1)
		{
			if (newSlotIndex == -1)
			{
				EmptySlot(slotIndex);
				FillSlot(slotIndex, newTileIndex);
			}
			else
			{
				EmptySlot(newSlotIndex);
				FillSlot(slotIndex, newTileIndex);
			}
		}
		else
		{
			if (newSlotIndex == -1)
			{
				EmptySlot(slotIndex);
				FillSlot(slotIndex, newTileIndex);
			}
			else
			{
				EmptySlot(slotIndex);
				EmptySlot(newSlotIndex);
				FillSlot(slotIndex, newTileIndex);
				FillSlot(newSlotIndex, tileIndex);
			}
		}
	}

	public int GetSlotTile(int slotIndex) => SlotTileBindings.ContainsForwardKey(slotIndex) ? SlotTileBindings.GetForwardValue(slotIndex) : -1;
	public int GetTileSlot(int tileIndex) => SlotTileBindings.ContainsBackwardKey(tileIndex) ? SlotTileBindings.GetBackwardValue(tileIndex) : -1;
}