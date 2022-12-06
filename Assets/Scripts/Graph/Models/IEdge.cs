public interface IEdge<R, T> where R : struct where T : IGraphData
{
	R Origin { get; }
	R Target { get; }
	T Data { get; }
}
