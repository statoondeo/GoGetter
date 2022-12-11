using System;
using System.Collections.Generic;

/// <summary>
/// Classe de base représentant un décorateur de noeud
/// </summary>
/// <typeparam name="R"></typeparam>
/// <typeparam name="T"></typeparam>
public abstract class BaseVertexDecorator<R, T> : IVertexDecorator<R, T>, IPoolable 
	where R : struct, IComparable where T : IGraphData, new()
{
	/// <summary>
	/// Id du noeud
	/// </summary>
	public R Id => InnerVertex.Id;

	/// <summary>
	/// Liste des connexions au départ du noeud
	/// </summary>
	public ISet<IEdge<R, T>> Edges => InnerVertex.Edges;

	/// <summary>
	/// Ajout d'une connexion au départ du noeud
	/// </summary>
	/// <param name="edge">Connexion à ajouter</param>
	/// <returns>La connexion ajoutée</returns>
	/// <exception cref="ArgumentNullException">La connexion est obligatoire</exception>
	public IEdge<R, T> AddEdge(IEdge<R, T> edge) => InnerVertex.AddEdge(edge);

	/// <summary>
	/// Suppression d'une connexion, si elle existe
	/// </summary>
	/// <param name="edge">Connexion à supprimer</param>
	public void RemoveEdge(IEdge<R, T> edge) => InnerVertex.RemoveEdge(edge);

	/// <summary>
	/// Est-ce que ce décorateur  est disponible
	/// </summary>
	public virtual bool IsAvailable => InnerVertex is null;

	/// <summary>
	/// Libération du décorateur
	/// </summary>
	public virtual void Reset()
	{
		InnerVertex = null;
		Path = null;
		Data = default;
		Visited = false;
	}

	/// <summary>
	/// Données de calcul pour le chemin en cours
	/// </summary>
	public T Data { get; set; }

	/// <summary>
	/// Le neoud a-t'il déjà été visité?
	/// </summary>
	public bool Visited { get; set; }

	/// <summary>
	/// Chemin le plus efficace passant par ce noeud
	/// </summary>
	public ISet<R> Path { get; set; }

	/// <summary>
	/// Noeud embarqué
	/// </summary>
	public IVertex<R, T> InnerVertex { get; protected set; }

	/// <summary>
	/// Mise à jour des données de chemin pendant le calcul
	/// </summary>
	/// <param name="data">Données de calcul</param>
	/// <param name="path">Chemin</param>
	public void Update(T data, ISet<R> path)
	{
		Data = data;
		Path = path;
	}

	/// <summary>
	/// Décoration proprement dite
	/// </summary>
	public void Wrap(IVertex<R, T> innerVertex)
	{
		InnerVertex = innerVertex;
		Update(new T(), new HashSet<R>() { Id });
	}

	/// <summary>
	/// Comparaison de noeud pour les traiter par ordre croissant d'efficacité
	/// </summary>
	/// <param name="other">un noeud</param>
	/// <returns>-1, 0 ou 1 suivant le résultat</returns>
	public int CompareTo(IVertexDecorator<R, T> other)
	{
		if (other is null) return (CompareTo(default));
		int comparaisonResult = Data.CompareTo(other.Data);
		if (comparaisonResult != 0) return (comparaisonResult);
		return (Id.CompareTo(other.Id));
	}

	/// <summary>
	/// Construction
	/// </summary>
	protected BaseVertexDecorator() { }
}
