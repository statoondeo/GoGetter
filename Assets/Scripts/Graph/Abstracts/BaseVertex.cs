using System;
using System.Collections.Generic;

/// <summary>
/// Classe de base représentant un noeud du graph
/// </summary>
/// <typeparam name="R">Type des identifiants des noeuds (types de base acceptés)</typeparam>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BaseVertex<R, T> : IVertex<R, T> where R : struct, IComparable where T : IGraphData
{
	/// <summary>
	/// Id du noeud
	/// </summary>
	public R Id { get; }

	/// <summary>
	/// Liste des connexions au départ du noeud
	/// </summary>
	public ISet<IEdge<R, T>> Edges { get; }

	/// <summary>
	/// Construction
	/// </summary>
	/// <param name="id">Id du noeud</param>
	protected BaseVertex(R id)
	{
		Id = id;
		Edges = new SortedSet<IEdge<R, T>>();
	}

	/// <summary>
	/// Ajout d'une connexion au départ du noeud
	/// </summary>
	/// <param name="edge">Connexion à ajouter</param>
	/// <returns>La connexion ajoutée</returns>
	/// <exception cref="ArgumentNullException">La connexion est obligatoire</exception>
	public IEdge<R, T> AddEdge(IEdge<R, T> edge)
	{
		if (edge is null) throw new ArgumentNullException(nameof(edge));
		Edges.Add(edge);
		return (edge);
	}

	/// <summary>
	/// Suppression d'une connexion, si elle existe
	/// </summary>
	/// <param name="edge">Connexion à supprimer</param>
	public void RemoveEdge(IEdge<R, T> edge)
	{
		if (Edges.Contains(edge)) Edges.Remove(edge);
	}
}
