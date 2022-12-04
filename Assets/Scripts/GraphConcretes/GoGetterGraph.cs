/// <summary>
/// Classe représentant le graph décrivant les chemins présents sur le plateau de jeu
/// </summary>
public sealed class GoGetterGraph : BaseGraph<GoGetterGraphData>
{
	/// <summary>
	/// Création du graph
	/// </summary>
	/// <param name="vertexFactory">Classe de création des noeuds</param>
	/// <param name="edgeFactory">Classe de création des connexions</param>
	public GoGetterGraph(IVertexFactory<GoGetterGraphData> vertexFactory, IEdgeFactory<GoGetterGraphData> edgeFactory)
		: base(vertexFactory, edgeFactory) 
	{
		// Création de tous les noeuds du plateau de jeu
		for (int i = 0; i < 24; i++) AddVertex(i + 1);
	}
}