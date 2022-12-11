using System;
using System.Collections.Generic;

public interface IVertex<R, T> where R : struct, IComparable where T : IGraphData
{
	R Id { get; }
	ISet<IEdge<R, T>> Edges { get; }
	IEdge<R, T> AddEdge(IEdge<R, T> edge);
	void RemoveEdge(IEdge<R, T> edges);
}
