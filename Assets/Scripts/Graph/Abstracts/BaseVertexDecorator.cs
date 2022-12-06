using System;
using System.Collections.Generic;

public abstract class BaseVertexDecorator<R, T> : IVertexDecorator<R, T> where R : struct where T : IGraphData, new()
{
	#region IVertex<R, T>

	public IVertex<R, T> InnerVertex { get; protected set; }
	public R Id => InnerVertex.Id;
	public IList<IEdge<R, T>> Edges => InnerVertex.Edges;
	public IEdge<R, T> AddEdge(IEdge<R, T> edge) => InnerVertex.AddEdge(edge);
	public void RemoveEdge(IEdge<R, T> edge) => InnerVertex.RemoveEdge(edge);

	#endregion

	public T Data { get; set; }
	public bool Visited { get; protected set; }
	public IList<R> Path { get; set; }
	public bool EverPerformed { get; set; }

	protected IGraph<R, T> Graph;

	protected BaseVertexDecorator(IGraph<R, T> graph, IVertex<R, T> innerVertex)
	{
		Graph = graph ?? throw new ArgumentNullException(nameof(graph));
		InnerVertex = innerVertex ?? throw new ArgumentNullException(nameof(innerVertex));
		Data = new T();
		Path = new List<R>() { Id };
	}
	public IList<R> Visit(IGraphConstraint<T> constraint)
	{
		if (null == constraint) throw new ArgumentNullException(nameof(constraint));

		IList<R> performedVertices = new List<R>();
		for (int i = 0; i < Edges.Count; i++)
		{
			IVertexDecorator<R, T> targetVertex = Graph.GetVertex(Edges[i].Target) as IVertexDecorator<R, T>;
			if (targetVertex.Visited || constraint.IsConstrainted((T)Data.Add(Edges[i].Data))) continue;
			if (targetVertex.EverPerformed && (Data.CompareTo(targetVertex.Data.Add(Edges[i].Data)) < 0)) continue;
			targetVertex.EverPerformed = true;
			targetVertex.Data = (T)Data.Add(Edges[i].Data);
			IList<R> newPath = new List<R>();
			((List<R>)newPath).AddRange(Path);
			((List<R>)newPath).AddRange(targetVertex.Path);
			targetVertex.Path = newPath;
			performedVertices.Add(targetVertex.Id);
		}
		Visited = true;
		return (performedVertices);
	}
}
