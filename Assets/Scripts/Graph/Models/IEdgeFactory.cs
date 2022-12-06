public interface IEdgeFactory<R, T> where R : struct where T : IGraphData
{
	IEdge<R, T> Create(R originId, R targetId, T data);
}
