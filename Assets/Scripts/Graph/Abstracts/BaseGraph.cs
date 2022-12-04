using System;
using System.Collections.Generic;

public abstract class BaseGraph<T> : IGraph<T> where T : class, IGraphData, new()
{
	protected IDictionary<int, IVertex<T>> Vertices;
	protected IVertexFactory<T> VertexFactory;
	protected IEdgeFactory<T> EdgeFactory;

	protected BaseGraph(IVertexFactory<T> vertexFactory, IEdgeFactory<T> edgeFactory)
	{
		Vertices = new Dictionary<int, IVertex<T>>();
		VertexFactory = vertexFactory ?? throw new ArgumentNullException(nameof(vertexFactory));
		EdgeFactory = edgeFactory ?? throw new ArgumentNullException(nameof(edgeFactory));
	}
	public virtual IVertex<T> AddVertex(int id)
	{
		IVertex<T> vertex = VertexFactory.Create(id);
		Vertices.Add(id, vertex);
		return (vertex);
	}
	public virtual IVertex<T> GetVertex(int id) => Vertices.ContainsKey(id) ? Vertices[id] : null;
	public virtual IEdge<T> AddEdge(int idOrigin, int idTarget, T data)
	{
		IVertex<T> originVertex = GetVertex(idOrigin) ?? AddVertex(idOrigin);
		IVertex<T> targetVertex = GetVertex(idTarget) ?? AddVertex(idTarget);
		return (originVertex.AddEdge(EdgeFactory.Create(targetVertex, data)));
	}
	public virtual void RemoveEdge(int idOrigin, int idTarget)
	{
		IVertex<T> vertex = GetVertex(idOrigin) ?? throw new ArgumentNullException(nameof(idOrigin));
		for (int i = 0; i < vertex.Edges.Count; i++)
		{
			IEdge<T> edge = vertex.Edges[i];
			if (edge.Target.Id == idTarget) edge.Origin.RemoveEdge(edge);
		}
	}
	public virtual T FindPath(int origin, int target, T failResult, IGraphConstraint<T> constraint)
	{
		if (null == failResult) throw new ArgumentNullException(nameof(failResult));
		if (null == constraint) throw new ArgumentNullException(nameof(constraint));

		IVertex<T> originVertex = GetVertex(origin);
		IVertex<T> targetVertex = GetVertex(target);

		if ((null == originVertex) || (null == targetVertex)) return (failResult);

		foreach (IVertex<T> vertex in Vertices.Values) vertex.Reset();

		HashSet<IVertex<T>> verticesToVisit = new() { originVertex };
		bool found = false;
		while (!found && (verticesToVisit.Count > 0))
		{
			IVertex<T> currentVertex = null;
			foreach (IVertex<T> vertex in verticesToVisit)
				if ((null == currentVertex) || (vertex.Data.CompareTo(currentVertex.Data) < 0)) currentVertex = vertex;
			verticesToVisit.Remove(currentVertex);
			verticesToVisit.UnionWith(currentVertex.Visit(constraint));
			found = currentVertex == targetVertex;
		}
		return (found ? targetVertex.Data : failResult);
	}
}
