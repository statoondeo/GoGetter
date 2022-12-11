using System;
using System.Collections.Generic;

/// <summary>
/// Classe décrivant un slot du plateau de jeu, c'est à dire les noeuds concernés par ce slot
/// </summary>
public sealed class GoGetterSlot
{
	private readonly IDictionary<int, int> Vertices;
	/// <summary>
	/// Création du slot
	/// </summary>
	/// <param name="vertices">Liste des noeuds concernés</param>
	/// <exception cref="ArgumentNullException">Si aucun noeud fourni</exception>
	public GoGetterSlot(params int[] vertices)
	{
		if ((null == vertices) || (0 == vertices.Length)) throw new ArgumentNullException(nameof(vertices));
		Vertices = new Dictionary<int, int>();
		for (int i = 0; i < vertices.Length; i++) Vertices.Add(i, vertices[i]);
	}
	/// <summary>
	/// Récupération de l'Id d'un noeud du slot
	/// </summary>
	/// <param name="key"></param>
	/// <returns>Id du noeud</returns>
	public int GetVertexId(int key) => Vertices[key];

	/// <summary>
	/// Est-ce que le slot contient les vertices demandés
	/// </summary>
	/// <param name="index1"></param>
	/// <param name="index2"></param>
	/// <returns>true si les 2 vertices sont dans le slot, false sinon</returns>
	public bool ContainsVertices(int index1, int index2) => Vertices.Values.Contains(index1) && Vertices.Values.Contains(index2);
}
