using System;
using System.Collections.Generic;

public abstract class BaseVertexDecorator<R, T> : IVertexDecorator<R, T> where R : struct where T : IGraphData, new()
{
	#region IVertex<R, T>

	public R Id => InnerVertex.Id;
	public IList<IEdge<R, T>> Edges => InnerVertex.Edges;
	public IEdge<R, T> AddEdge(IEdge<R, T> edge) => InnerVertex.AddEdge(edge);
	public void RemoveEdge(IEdge<R, T> edge) => InnerVertex.RemoveEdge(edge);

	#endregion

	public IVertex<R, T> InnerVertex { get; protected set; }
	public T Data { get; set; }
	public bool Visited { get; protected set; }
	public IList<R> Path { get; set; }

	protected readonly IGraph<R, T> Graph;

	protected BaseVertexDecorator(IGraph<R, T> graph, IVertex<R, T> innerVertex)
	{
		Graph = graph ?? throw new ArgumentNullException(nameof(graph));
		InnerVertex = innerVertex ?? throw new ArgumentNullException(nameof(innerVertex));
		Path = new List<R>() { Id };
	}
	public IList<R> Visit(IGraphConstraint<T> constraint)
	{
		if (null == constraint) throw new ArgumentNullException(nameof(constraint));

		IList<R> exploredVertices = new List<R>();
		for (int i = 0; i < Edges.Count; i++)
		{
			IEdge<R, T> edge = Edges[i];
			IVertexDecorator<R, T> targetVertex = Graph.GetVertex(edge.Target) as IVertexDecorator<R, T>;
			if ((null == targetVertex) || targetVertex.Visited || constraint.IsConstrainted((T)edge.Data.Add(Data))) continue;
			if ((null != targetVertex.Data) && (edge.Data.Add(targetVertex.Data).CompareTo(Data) > 0)) continue;

			targetVertex.Data = (T)edge.Data.Add(Data);
			List<R> newPath = new();
			newPath.AddRange(Path);
			newPath.AddRange(targetVertex.Path);
			targetVertex.Path = newPath;
			exploredVertices.Add(targetVertex.Id);
		}
		Visited = true;
		return (exploredVertices);
	}
}
