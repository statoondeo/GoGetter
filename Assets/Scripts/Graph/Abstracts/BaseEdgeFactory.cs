public abstract class BaseEdgeFactory<R, T> : IEdgeFactory<R, T> where R : struct where T : IGraphData
{
	public abstract IEdge<R, T> Create(R originId, R targetId, T data);
}