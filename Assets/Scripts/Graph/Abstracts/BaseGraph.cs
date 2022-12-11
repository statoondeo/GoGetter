using System;
using System.Collections.Generic;

/// <summary>
/// Classe de base permettant de gérer un graph pour la recherche du plus court chemin
/// entre 2 noeuds.
/// </summary>
/// <typeparam name="R">Type des identifiants des noeuds (types de base acceptés)</typeparam>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BaseGraph<R, T> : IGraph<R, T> where R : struct, IComparable where T : IGraphData
{
	protected readonly IList<IVertexDecorator<R, T>> DecoratedVertices;
	protected readonly IDictionary<R, IVertex<R, T>> Vertices;
	protected readonly IVertexFactory<R, T> VertexFactory;
	protected readonly IPoolManager VertexDecoratorPoolManager;
	protected readonly IEdgeFactory<R, T> EdgeFactory;
	protected readonly IGraphConstraint<T> DefaultConstraint;

	/// <summary>
	/// Construction du graph
	/// </summary>
	/// <param name="vertexFactory">Factory des noeuds</param>
	/// <param name="vertexDecoratorPoolManager">Factory des décorateurs de noeud</param>
	/// <param name="edgeFactory">Factory des connexions</param>
	/// <param name="defaultConstraint">Contrainte par défaut pour le calcul du plus court chemin</param>
	/// <exception cref="ArgumentNullException">Si l'un des paramètres est absent</exception>
	protected BaseGraph(
		IVertexFactory<R, T> vertexFactory,
		IPoolManager vertexDecoratorPoolManager,
		IEdgeFactory<R, T> edgeFactory,
		IGraphConstraint<T> defaultConstraint)
	{
		VertexFactory = vertexFactory ?? throw new ArgumentNullException(nameof(vertexFactory));
		VertexDecoratorPoolManager = vertexDecoratorPoolManager ?? throw new ArgumentNullException(nameof(vertexDecoratorPoolManager));
		EdgeFactory = edgeFactory ?? throw new ArgumentNullException(nameof(edgeFactory));
		DefaultConstraint = defaultConstraint ?? throw new ArgumentNullException(nameof(defaultConstraint));
		Vertices = new Dictionary<R, IVertex<R, T>>();
		DecoratedVertices = new List<IVertexDecorator<R, T>>();
	}

	/// <summary>
	/// Ajout d'un noeud au graph, représenté par son Id
	/// </summary>
	/// <param name="id">Id du noeud</param>
	/// <returns>Le noeud créé</returns>
	public virtual IVertex<R, T> AddVertex(R id)
	{
		IVertex<R, T> vertex = VertexFactory.Create(id);
		Vertices.Add(id, vertex);
		return (vertex);
	}

	/// <summary>
	/// Récupération d'un noeud à partir de son Id
	/// </summary>
	/// <param name="id">Id du noeud</param>
	/// <returns>Le noeud recherché ou null si non présent</returns>
	public virtual IVertex<R, T> GetVertex(R id) => Vertices.ContainsKey(id) ? Vertices[id] : null;

	/// <summary>
	/// Ajout d'une connexion au graph
	/// Si les noeuds concernés n'existent pas ils sont créés
	/// </summary>
	/// <param name="originId">Id du noeud d'origine</param>
	/// <param name="targetId">Id du noeud destination</param>
	/// <param name="data">Informations portées par cette connexion</param>
	/// <returns>La connexion créée</returns>
	public virtual IEdge<R, T> AddEdge(R originId, R targetId, T data)
	{
		IVertex<R, T> originVertex = GetVertex(originId) ?? AddVertex(originId);
		IVertex<R, T> targetVertex = GetVertex(targetId) ?? AddVertex(targetId);
		return (originVertex.AddEdge(EdgeFactory.Create(originVertex.Id, targetVertex.Id, data)));
	}

	/// <summary>
	/// Suppression d'une connexion du graph
	/// </summary>
	/// <param name="originId">Id du noeud d'origine</param>
	/// <param name="targetId">Id du noeud destination</param>
	/// <exception cref="ArgumentNullException">Si le noeud d'origine n'existe pas</exception>
	public virtual void RemoveEdge(R originId, R targetId)
	{
		IVertex<R, T> vertex = GetVertex(originId) ?? throw new ArgumentNullException(nameof(originId));
		foreach (IEdge<R, T> edge in vertex.Edges)
		{
			if (edge.Target.Equals(targetId))
			{
				vertex.RemoveEdge(edge);
				break;
			}
		}
	}

	/// <summary>
	/// Traitement d'un noeud lors du calcul d'un chemin.
	/// Ce traitement consiste à parcourir les différentes connexions de ce noeud vers des noeuds non visités,
	/// afin de propager les informations en cours de calcul pour mettre à jour les noeuds destinataires si
	/// ces informations sont considérées comme plus avantageuses pour le calcul.
	/// Les connexions du noeud sont parcourues par ordre croissant de leurs informations.
	/// </summary>
	/// <param name="vertex">Noeud à visiter</param>
	/// <param name="constraint">Contraintes à respecter pour le calcul du chemin le plus efficace</param>
	/// <returns>Liste des Id des noeuds explorés</returns>
	protected virtual ISet<IVertexDecorator<R, T>> VisitVertex(IVertexDecorator<R, T> vertex, IGraphConstraint<T> constraint)
	{
		// Liste des noeuds explorés depuis ce noeud
		ISet<IVertexDecorator<R, T>> exploredVertices = new SortedSet<IVertexDecorator<R, T>>();

		// Exploration de chaque connexion, en traitant chaque destination
		foreach (IEdge<R, T> edge in vertex.Edges)
		{
			// Noeud non encore eexploré, il faut le décorer
			if (GetVertex(edge.Target) is not IVertexDecorator<R, T> targetVertex)
			{
				targetVertex = DecorateVertex(GetVertex(edge.Target));
				Vertices[targetVertex.Id] = targetVertex;
			}

			// Si le noeud à explorer a déja été visité ou ne remplit pas les contraintes à respecter on passe au suivant
			if (targetVertex.Visited || constraint.IsConstrainted((T)edge.Data.Add(vertex.Data))) continue;

			// Si le noeud a déjà été exploré mais qu'il n'améliore pas le chemin en cours, on passe au suivant
			IGraphData comparisonData = edge.Data.Add(vertex.Data);
			if (targetVertex.Data is not null && (comparisonData.CompareTo(targetVertex.Data) > 0)) continue;

			// Le noeud est intéressant pour le chemin en cours
			targetVertex.Update((T)comparisonData, new HashSet<R>(vertex.Path) { targetVertex.Id });

			// Il faudra visiter ce noeud pour calculer le meilleur chemin
			exploredVertices.Add(targetVertex);
		}
		vertex.Visited = true;
		return (exploredVertices);
	}

	/// <summary>
	/// Recherche du chemin le plus efficace entre 2 noeuds
	/// </summary>
	/// <param name="origin">Id du noeud de départ</param>
	/// <param name="target">Id du noeud d'arrivée</param>
	/// <param name="constraint">Contraintes à respecter pour le calcul</param>
	/// <returns>Chemin le plus efficace sous la forme d'une liste d'Id de noeud</returns>
	public virtual ISet<R> FindPath(R origin, R target, IGraphConstraint<T> constraint = null)
	{
		// Sélection des contraintes à appliquer
		IGraphConstraint<T> localConstraint = constraint ?? DefaultConstraint;

		// Si les noeuds origine ou destination n'existent pas, il n'y a pas de chemin existant
		IVertex<R, T> originVertex = GetVertex(origin);
		if (originVertex is null || GetVertex(target) is null) return (null);

		// On commence par le noeud origine
		IVertexDecorator<R, T> originVertexDecorator = DecorateVertex(originVertex);
		originVertexDecorator.Data.Clear();
		ISet<IVertexDecorator<R, T>> verticesToVisit = new SortedSet<IVertexDecorator<R, T>>() { originVertexDecorator };

		// Tant que le parcours de proche en proche contient des noeuds non visités
		while (verticesToVisit.Count > 0)
		{
			// parmi les noeuds à visiter, on traite toujours celui qui fait partie du chemin le plus efficace,
			// donc le 1er de la liste et on le supprime des noeuds à traiter
			IEnumerator<IVertexDecorator<R, T>> enumerator = verticesToVisit.GetEnumerator();
			enumerator.MoveNext();
			IVertexDecorator<R, T> currentVertex = enumerator.Current;
			verticesToVisit.Remove(currentVertex);

			// On visite le noeud sélectionné
			verticesToVisit.UnionWith(VisitVertex(currentVertex, localConstraint));

			// Si la destination n'est pas encore atteinte, on poursuit le parcours
			if (!currentVertex.Id.Equals(target)) continue;

			// Sinon on retourne le chemin trouvé
			ISet<R> path = currentVertex.Path;
			ResetVertices();
			return (path);
		}
		// Si aucun chemin n'a été trouvé
		ResetVertices();
		return (null);
	}

	/// <summary>
	/// Décoration d'un noeud pour l'explorer
	/// </summary>
	/// <param name="vertex">Le noeud à décorer</param>
	/// <returns>Le noeud décoré</returns>
	protected virtual IVertexDecorator<R, T> DecorateVertex(IVertex<R, T> vertex)
	{
		// Décoration du noeud
		IVertexDecorator<R, T> decoratedVertex = VertexDecoratorPoolManager.GetNew()as IVertexDecorator<R, T>;
		decoratedVertex.Wrap(vertex);

		// Enregistrement dans la liste des décorateurs
		DecoratedVertices.Add(decoratedVertex);

		// Changement dans la liste des noeuds
		Vertices[decoratedVertex.Id] = decoratedVertex;

		// Retour du noeud décoré
		return (decoratedVertex);
	}

	/// <summary>
	/// Nettoyage du graph, c'est à dire suppression des décorateurs installés sur les noeuds explorés
	/// </summary>
	protected virtual void ResetVertices()
	{
		// Traitement des noeuds décorés
		for (int i = 0; i < DecoratedVertices.Count; i++)
		{
			// Suppression du décorateur
			IVertexDecorator<R, T> decoratedVertex = DecoratedVertices[i];
			Vertices[decoratedVertex.Id] = decoratedVertex.InnerVertex;

			// Mise à disposition du décorateur
			((IPoolable)decoratedVertex).Reset();
		}
		DecoratedVertices.Clear();
	}
}
