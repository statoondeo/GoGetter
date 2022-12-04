/// <summary>
/// Classe définissant une connexion dans le graph du plateau de jeu
/// </summary>
public sealed class GoGetterEdge : BaseEdge<GoGetterGraphData>
{
	/// <summary>
	/// Création de la connexion
	/// </summary>
	/// <param name="target">Noeud destination</param>
	/// <param name="data">informations de l connexion</param>
	public GoGetterEdge(IVertex<GoGetterGraphData> target, GoGetterGraphData data) : base(target, data) { }
}