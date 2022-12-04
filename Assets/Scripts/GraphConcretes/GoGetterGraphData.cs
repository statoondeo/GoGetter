using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Classe décrivant les données attachées à chaque connexion entre les noeuds du graph
/// </summary>
public sealed class GoGetterGraphData : BaseGraphData
{
	/// <summary>
	/// Nombre d'étapes de la connexion ou du chemin
	/// </summary>
	public int Step { get; private set; }
	/// <summary>
	/// Création des données des noeuds
	/// </summary>
	public GoGetterGraphData() : this(0) { }
	/// <summary>
	/// Création des données des connexions
	/// </summary>
	/// <param name="step">Coût de la connexion</param>
	public GoGetterGraphData(int step) => Step = step;
	/// <summary>
	/// Cumul de données entre elles
	/// </summary>
	/// <param name="data">Liste des données à cumuler</param>
	/// <returns>Données cumulées</returns>
	public override IGraphData Add(params IGraphData[] data)
	{
		if (null == data) return (this);
		int cost = Step;
		for (int i = 0; i < data.Length; i++) cost += ((GoGetterGraphData)data[i]).Step;
		return (new GoGetterGraphData(cost));
	}
	/// <summary>
	/// Remise à zéro
	/// </summary>
	public override void Reset() => Step = 0;
	/// <summary>
	/// Comparaison de données entre elles
	/// </summary>
	/// <param name="other">La donnée à comparer à la donnée courante</param>
	/// <returns>-1 si la donnée courante est plus petite, 0 si égalité et 1 si elle est plus grande</returns>
	public override int CompareTo([AllowNull] IGraphData other) => null == other ? Step.CompareTo(null) : Step.CompareTo(((GoGetterGraphData)other).Step);
}
