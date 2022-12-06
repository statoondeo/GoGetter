public abstract class BaseVertexDecoratorFactory<R, T> : IVertexDecoratorFactory<R, T> where R : struct where T : IGraphData
{
	public abstract IVertexDecorator<R, T> Create(IGraph<R, T> graph, IVertex<R, T> innerVertex);
}