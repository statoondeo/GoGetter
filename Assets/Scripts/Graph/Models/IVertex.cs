using System.Collections.Generic;

public interface IVertex<T> where T : IGraphData
{
	int Id { get; }
	T Data { get; }
	IList<IEdge<T>> Edges { get; }
	bool Visited { get; }
	void Reset();
	IEdge<T> AddEdge(IEdge<T> edge);
	void RemoveEdge(IEdge<T> edges);
	bool PerformEdge(IEdge<T> edge);
	IList<IVertex<T>> Visit(IGraphConstraint<T> constraint);
}
