using System;
using System.Collections.Generic;

public abstract class BaseVertex<R, T> : IVertex<R, T> where R : struct where T : IGraphData
{
	public R Id { get; protected set; }
	public IList<IEdge<R, T>> Edges { get; protected set; }
	protected BaseVertex(R id)
	{
		Id = id;
		Edges = new List<IEdge<R, T>>();
	}
	public IEdge<R, T> AddEdge(IEdge<R, T> edge)
	{
		if (null == edge) throw new ArgumentNullException(nameof(edge));

		Edges.Add(edge);
		return (edge);
	}
	public void RemoveEdge(IEdge<R, T> edge)
	{
		if (Edges.Contains(edge)) Edges.Remove(edge);
	}
}
