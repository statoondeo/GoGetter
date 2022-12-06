using System.Collections.Generic;

public interface IGraph<R, T> where R : struct where T : IGraphData
{
	IVertex<R, T> GetVertex(R id);
	IVertex<R, T> AddVertex(R id);
	IEdge<R, T> AddEdge(R originId, R targetid, T data);
	void RemoveEdge(R originId, R targetid);
	IList<R> FindPath(R originId, R targetid, IGraphConstraint<T> constraint = null);
}