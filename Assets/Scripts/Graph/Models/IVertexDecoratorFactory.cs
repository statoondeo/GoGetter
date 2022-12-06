public interface IVertexDecoratorFactory<R, T> where R : struct where T : IGraphData
{
	IVertexDecorator<R, T> Create(IGraph<R, T> graph, IVertex<R, T> innerVertex);
}