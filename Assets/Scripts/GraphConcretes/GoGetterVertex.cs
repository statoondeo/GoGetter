/// <summary>
/// Classe définissant un noeud dans le graph du plateau de jeu
/// </summary>
public sealed class GoGetterVertex : BaseVertex<GoGetterGraphData>
{
	/// <summary>
	/// Création d'un noeud
	/// </summary>
	/// <param name="id">Id du noeud</param>
	public GoGetterVertex(int id) : base(id) { }
}
