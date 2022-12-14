using System;
using System.Collections.Generic;

/// <summary>
/// Classe décrivant une tuile du plateau de jeu, c'est à dire les connexions disponibles sur cette tuile.
/// La tuile peut être tournée, les connexions sont recalculées.
/// </summary>
public sealed class GoGetterTile
{
	private readonly IList<(int, int)> Edges;
	private const int NB_FACES = 4;
	private int Rotation;

	/// <summary>
	/// Création de la tuile
	/// </summary>
	/// <param name="edges">Liste des connexions disponibles</param>
	/// <exception cref="ArgumentNullException">Si aucun connexion</exception>
	public GoGetterTile(params (int, int)[] edges)
	{
		if ((null == edges) || (0 == edges.Length)) throw new ArgumentNullException(nameof(edges));
		Rotation = 0;
		Edges = new List<(int, int)>();
		for (int i = 0; i < edges.Length; i++)
		{
			// Gestion des connexions non orientées
			Edges.Add((edges[i].Item2, edges[i].Item1));
			Edges.Add(edges[i]);
		}
	}

	/// <summary>
	/// Nombre de connexion de la tuile
	/// </summary>
	public int EdgeCount => Edges.Count;

	/// <summary>
	/// Récupération d'une connexion, avec prise e ncopte de la rotation à appliquer
	/// </summary>
	/// <param name="index">Index de la connexion</param>
	/// <returns>le couple (index origine, index destination)</returns>
	public (int, int) GetEdge(int index) => ((Edges[index].Item1 + Rotation) % NB_FACES, (Edges[index].Item2 + Rotation) % NB_FACES);

	/// <summary>
	/// Rotation de la tuile, utilisée dans le calcul des connexions
	/// </summary>
	public void Rotate() => Rotation = (Rotation + 1) % NB_FACES;
}
