public interface IEdge<T> where T : IGraphData
{
	IVertex<T> Origin { get; }
	IVertex<T> Target { get; }
	T Data { get; }

	bool Follow(IGraphConstraint<T> constraint);
	void SetOrigin(IVertex<T> vertex);
}
