/// <summary>
/// Classe de base représentant les données portées par une connexion
/// </summary>
public abstract class BaseGraphData : IGraphData
{
	/// <summary>
	/// Construction
	/// </summary>
	protected BaseGraphData() { }

	/// <summary>
	/// Opération de cumul entre données
	/// </summary>
	/// <param name="data">Données à cumuler à la donnée courante</param>
	/// <returns>Données cumulées</returns>
	public abstract IGraphData Add(IGraphData data);

	/// <summary>
	/// Remiseà zéro de toutes les informations
	/// </summary>
	/// <exception cref="System.NotImplementedException"></exception>
	public abstract void Clear();

	/// <summary>
	/// Comparaison de données entre elles
	/// </summary>
	/// <param name="other">Données à comparer aux données courantes</param>
	/// <returns>-1, 0 ou 1 suivant les valeurs comparées</returns>
	public virtual int CompareTo(IGraphData other) => 0;
}