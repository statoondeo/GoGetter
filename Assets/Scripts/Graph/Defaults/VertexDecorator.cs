public sealed class VertexDecorator : BaseVertexDecorator<int, GraphData>
{
	public VertexDecorator(IGraph<int, GraphData> graph, IVertex<int, GraphData> innerVertex) : base(graph, innerVertex) { }
}
