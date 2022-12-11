using System;
/// <summary>
/// Classe de base représentant l'usine de création de noeud
/// </summary>
/// <typeparam name="R">Type des identifiants des noeuds (types de base acceptés)</typeparam>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BaseVertexFactory<R, T> : IVertexFactory<R, T> where R : struct, IComparable where T : IGraphData
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public abstract IVertex<R, T> Create(R id);
}
