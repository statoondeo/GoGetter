public class VertexFactory : BaseVertexFactory<int, GraphData>
{
	public override IVertex<int, GraphData> Create(int id) => new Vertex(id);
}
