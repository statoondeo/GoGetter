public interface IEdgeFactory<T> where T : class, IGraphData
{
	IEdge<T> Create(IVertex<T> targetVertex, T data);
}
