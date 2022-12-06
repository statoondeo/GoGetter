using System;
using System.Collections.Generic;
using System.Text;

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
	public bool ContainsVertices(int index1, int index2) => Vertices.Values.Contains(index1) && Vertices.Values.Contains(index2);

	public override string ToString()
	{
		StringBuilder sb = new();
		sb.AppendLine($"Tuile=>");
		IEnumerator<int> enumerator = Vertices.Values.GetEnumerator();
		while(enumerator.MoveNext()) sb.Append($"{enumerator.Current}");
		return (sb.ToString());
	}
}
