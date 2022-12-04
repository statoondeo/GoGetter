public abstract class BaseEdgeFactory<T> : IEdgeFactory<T> where T : class, IGraphData, new()
{
	public abstract IEdge<T> Create(IVertex<T> targetVertex, T data);
}