/// <summary>
/// Classe permettant la création des noeuds du graph du plateau de jeu
/// </summary>
public sealed class GoGetterVertexFactory : BaseVertexFactory<GoGetterGraphData>
{
	/// <summary>
	/// Création d'un noeud
	/// </summary>
	/// <param name="id">Id du noeud</param>
	/// <returns>Noeud créé</returns>
	public override IVertex<GoGetterGraphData> Create(int id) => new GoGetterVertex(id);
}
