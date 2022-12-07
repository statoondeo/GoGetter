using System;
using System.Collections.Generic;

public abstract class BaseGraph<R, T> : IGraph<R, T> where R : struct where T : IGraphData
{
	protected readonly IDictionary<R, IVertex<R, T>> Vertices;
	protected readonly IVertexFactory<R, T> VertexFactory;
	protected readonly IVertexDecoratorFactory<R, T> VertexDecoratorFactory;
	protected readonly IEdgeFactory<R, T> EdgeFactory;
	protected readonly IGraphConstraint<T> DefaultConstraint;
	protected BaseGraph(IVertexFactory<R, T> vertexFactory, IVertexDecoratorFactory<R, T> vertexDecoratorFactory, IEdgeFactory<R, T> edgeFactory, IGraphConstraint<T> defaultConstraint)
	{
		VertexFactory = vertexFactory ?? throw new ArgumentNullException(nameof(vertexFactory));
		VertexDecoratorFactory = vertexDecoratorFactory ?? throw new ArgumentNullException(nameof(vertexDecoratorFactory));
		EdgeFactory = edgeFactory ?? throw new ArgumentNullException(nameof(edgeFactory));
		DefaultConstraint = defaultConstraint ?? throw new ArgumentNullException(nameof(defaultConstraint));
		Vertices = new Dictionary<R, IVertex<R, T>>();
	}
	public virtual IVertex<R, T> AddVertex(R id)
	{
		IVertex<R, T> vertex = VertexFactory.Create(id);
		Vertices.Add(id, vertex);
		return (vertex);
	}
	public virtual IVertex<R, T> GetVertex(R id) => Vertices.ContainsKey(id) ? Vertices[id] : null;
	public virtual IEdge<R, T> AddEdge(R originId, R targetId, T data)
	{
		IVertex<R, T> originVertex = GetVertex(originId) ?? AddVertex(originId);
		IVertex<R, T> targetVertex = GetVertex(targetId) ?? AddVertex(targetId);
		return (originVertex.AddEdge(EdgeFactory.Create(originVertex.Id, targetVertex.Id, data)));
	}
	public virtual void RemoveEdge(R originId, R targetId)
	{
		IVertex<R, T> vertex = GetVertex(originId) ?? throw new ArgumentNullException(nameof(originId));
		for (int i = 0; i < vertex.Edges.Count; i++)
		{
			IEdge<R, T> edge = vertex.Edges[i];
			if (edge.Target.Equals(targetId)) vertex.RemoveEdge(edge);
		}
	}
	protected virtual void DecorateVertices()
	{
		IList<R> vertexKeysList = new List<R>();
		foreach (R key in Vertices.Keys) vertexKeysList.Add(key);
		for (int i = 0; i < vertexKeysList.Count; i++) Vertices[vertexKeysList[i]] = VertexDecoratorFactory.Create(this, Vertices[vertexKeysList[i]]);
	}
	protected virtual void UnDecorateVertices()
	{
		IList<R> vertexKeysList = new List<R>();
		foreach (R key in Vertices.Keys) vertexKeysList.Add(key);
		for (int i = 0; i < vertexKeysList.Count; i++) Vertices[vertexKeysList[i]] = (Vertices[vertexKeysList[i]] as IVertexDecorator<R, T>).InnerVertex;
	}
	public virtual IList<R> FindPath(R origin, R target, IGraphConstraint<T> constraint = null)
	{
		IGraphConstraint<T> localConstraint = constraint ?? DefaultConstraint;
		if ((null == GetVertex(origin)) || (null == GetVertex(target))) return (null);
		DecorateVertices();
		IVertexDecorator<R, T> currentVertex = null;
		HashSet<R> verticesToVisit = new() { origin };
		bool found = false;
		while (!found && (verticesToVisit.Count > 0))
		{
			currentVertex = null;
			foreach (R vertexId in verticesToVisit)
			{
				IVertexDecorator<R, T> vertex = GetVertex(vertexId) as IVertexDecorator<R, T>;
				if ((null == currentVertex) || (vertex.Data.CompareTo(currentVertex.Data) < 0)) currentVertex = vertex;
			}
			verticesToVisit.Remove(currentVertex.Id);
			verticesToVisit.UnionWith(currentVertex.Visit(localConstraint));
			found = currentVertex.Id.Equals(target);
		}
		IList<R> path = found ? currentVertex.Path : null;
		UnDecorateVertices();
		return (path);
	}
}
