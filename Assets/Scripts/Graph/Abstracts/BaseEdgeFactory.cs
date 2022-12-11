using System;

/// <summary>
/// Classe de base représentant l'usine de création de connexion
/// </summary>
/// <typeparam name="R">Type des identifiants des noeuds (types de base acceptés)</typeparam>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BaseEdgeFactory<R, T> : IEdgeFactory<R, T> where R : struct, IComparable where T : IGraphData
{
	/// <summary>
	/// Construction de la connexion
	/// </summary>
	/// <param name="originId">Id du noeud origine</param>
	/// <param name="targetId">Id du noeud destination</param>
	/// <param name="data">Informations potées par la connexion</param>
	/// <returns>La connexion créée</returns>
	public abstract IEdge<R, T> Create(R originId, R targetId, T data);
}