using System;
using System.Collections.Generic;

public interface IGraph<R, T> where R : struct, IComparable where T : IGraphData
{
	IVertex<R, T> GetVertex(R id);
	IVertex<R, T> AddVertex(R id);
	IEdge<R, T> AddEdge(R originId, R targetid, T data);
	void RemoveEdge(R originId, R targetid);
	ISet<R> FindPath(R originId, R targetid, IGraphConstraint<T> constraint = null);
}