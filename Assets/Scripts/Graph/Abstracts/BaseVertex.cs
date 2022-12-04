using System;
using System.Collections.Generic;

public abstract class BaseVertex<T> : IVertex<T> where T : IGraphData, new()
{
	public int Id { get; protected set; }
	public T Data { get; protected set; }
	public bool Visited { get; protected set; }
	public IList<IEdge<T>> Edges { get; protected set; }

	protected bool EverPerformed;

	protected BaseVertex(int id)
	{
		Id = id;
		Data = new T();
		Edges = new List<IEdge<T>>();
		Reset();
	}
	public IList<IVertex<T>> Visit(IGraphConstraint<T> constraint)
	{
		if (null == constraint) throw new ArgumentNullException(nameof(constraint));

		SortedList<(int, T), IVertex<T>> performedVertices = new();
		for (int i = 0; i < Edges.Count; i++)
		{
			if (Edges[i].Target.Visited) continue;
			IEdge<T> currentEdge = Edges[i];
			if (currentEdge.Follow(constraint)) performedVertices.Add((currentEdge.Target.Id, currentEdge.Data), currentEdge.Target);
		}
		Visited = true;
		return (performedVertices.Values);
	}
	public IEdge<T> AddEdge(IEdge<T> edge)
	{
		if (null == edge) throw new ArgumentNullException(nameof(edge));

		edge.SetOrigin(this);
		Edges.Add(edge);
		return (edge);
	}
	public void RemoveEdge(IEdge<T> edge)
	{
		if (Edges.Contains(edge)) Edges.Remove(edge);
	}
	public bool PerformEdge(IEdge<T> edge)
	{
		if ((null == edge) || (EverPerformed && (Data.CompareTo(edge.Origin.Data.Add(edge.Data)) < 0))) return (false);

		EverPerformed = true;
		Data = (T)edge.Origin.Data.Add(edge.Data);
		return (true);
	}
	public void Reset()
	{
		Data.Reset();
		Visited = false;
		EverPerformed = false;
	}
}
