using System.Collections.Generic;

/// <summary>
/// Données du jeu
/// </summary>
public sealed class GoGetterData
{
	/// <summary>
	/// Liste des index des items du plateau de jeu
	/// </summary>
	/// <returns>Index des items</returns>
	public static IList<int> GetItemIds() => new List<int> { 1, 2, 3, 4, 7, 11, 14, 18, 21, 22, 23, 24 };
	/// <summary>
	/// Liste des connexions du plateau de jeu
	/// </summary>
	/// <returns>Index des connexions</returns>
	public static IList<int> GetConnexionsIds() => new List<int> { 5, 6, 8, 9, 10, 12, 13, 15, 16, 17, 19, 20 };
	/// <summary>
	/// Compositions des slots du plateau de jeu
	/// </summary>
	/// <returns>Liste des slots</returns>
	public static IList<GoGetterSlot> GetSlots()
		=> new List<GoGetterSlot>
		{
			new GoGetterSlot(1, 5, 8, 4),
			new GoGetterSlot(2, 6, 9, 5),
			new GoGetterSlot(3, 7, 10, 6),
			new GoGetterSlot(8, 12, 15, 11),
			new GoGetterSlot(9, 13, 16, 12),
			new GoGetterSlot(10, 14, 17, 13),
			new GoGetterSlot(15, 19, 22, 18),
			new GoGetterSlot(16, 20, 23, 19),
			new GoGetterSlot(17, 21, 24, 20)
		};
	/// <summary>
	/// Compositions des tuiles du jeu (ici spécifiques à la version chat et souris)
	/// </summary>
	/// <returns>Lite des tuiles</returns>
	public static IList<GoGetterTile> GetTiles()
		=> new List<GoGetterTile>
		{
			new GoGetterTile((2, 3), (3, 2)),
			new GoGetterTile((0, 3), (3, 0)),
			new GoGetterTile((1, 2), (2, 1)),
			new GoGetterTile((0, 1), (1, 0), (2, 3), (3, 2)),
			new GoGetterTile((0, 2), (2, 0), (1, 3), (3, 1)),
			new GoGetterTile((0, 1), (0, 3), (1, 0), (3, 0)),
			new GoGetterTile((0, 3), (3, 0), (1, 2), (2, 1)),
			new GoGetterTile((0, 1), (0, 2), (1, 0), (1, 2), (2, 0), (2, 1)),
			new GoGetterTile((0, 1), (0, 2), (0, 3), (1, 0), (1, 2), (1, 3), (2, 0), (2, 1), (2, 3), (3, 0), (3, 1), (3, 2)),
		};
}
