public sealed class VertexDecoratorFactory : BaseVertexDecoratorFactory<int, GraphData>
{
	public override IVertexDecorator<int, GraphData> Create(IGraph<int, GraphData> graph, IVertex<int, GraphData> innerVertex)
		=> new VertexDecorator(graph, innerVertex);
}