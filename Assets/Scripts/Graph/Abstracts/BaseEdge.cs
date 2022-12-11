using System;

/// <summary>
/// Classede base représentant une connexion
/// </summary>
/// <typeparam name="R">Type des identifiants des noeuds (types de base acceptés)</typeparam>
/// <typeparam name="T">Type des données attachées aux connexions (poids, temps, etc)</typeparam>
public abstract class BaseEdge<R, T> : IEdge<R, T> where R : struct, IComparable where T : IGraphData
{
	/// <summary>
	/// Id du noeud origine
	/// </summary>
	public R Origin { get; }

	/// <summary>
	/// Id du noeud destination
	/// </summary>
	public R Target { get; }

	/// <summary>
	/// Informations potées par la connexion
	/// </summary>
	public T Data { get; }

	/// <summary>
	/// Construction de la connexion
	/// </summary>
	/// <param name="originId">Id du noeud origine</param>
	/// <param name="targetId">Id du noeud destination</param>
	/// <param name="data">Informations potées par la connexion</param>
	/// <exception cref="ArgumentNullException">Les données sont obligatoires</exception>
	protected BaseEdge(R originId, R targetId, T data)	{
		Data = data ?? throw new ArgumentNullException(nameof(data));
		Origin = originId;
		Target = targetId;
	}

	/// <summary>
	/// Comparaison de connexion pour les traiter par ordre croissant d'efficacité
	/// </summary>
	/// <param name="other">une connexion</param>
	/// <returns>-1, 0 ou 1 suivant le résultat</returns>
	public int CompareTo(IEdge<R, T> other)
	{
		if (other is null) return (CompareTo(default));
		int comparaisonResult = Data.CompareTo(other.Data);
		if (comparaisonResult != 0) return comparaisonResult;
		return (Target.CompareTo(other.Target));
	}
}
