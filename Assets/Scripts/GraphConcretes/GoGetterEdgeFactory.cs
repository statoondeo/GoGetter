/// <summary>
/// Classe permettant la création des connexions du graph du plateau de jeu
/// </summary>
public sealed class GoGetterEdgeFactory : BaseEdgeFactory<GoGetterGraphData>
{
	/// <summary>
	/// Création de la connexion
	/// </summary>
	/// <param name="targetVertex">Noeud destination</param>
	/// <param name="data">informations de la connexion</param>
	/// <returns>La connexioncréée</returns>
	public override IEdge<GoGetterGraphData> Create(IVertex<GoGetterGraphData> targetVertex, GoGetterGraphData data)
		=> new GoGetterEdge(targetVertex, data);
}