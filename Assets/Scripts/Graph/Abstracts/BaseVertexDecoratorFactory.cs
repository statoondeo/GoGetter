using System;

/// <summary>
/// Classe de base représentant l'usine de création de décoratur de noeud
/// </summary>
/// <typeparam name="R">Type des identifiants des noeuds (types de base acceptés)</typeparam>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BaseVertexDecoratorFactory<R, T> : IVertexDecoratorFactory<R, T> where R : struct, IComparable where T : IGraphData
{
	/// <summary>
	/// Création
	/// </summary>
	/// <returns>Le décorateur créé</returns>
	public abstract IPoolable Create();
}