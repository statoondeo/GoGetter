using System;
using System.Collections.Generic;

public interface IVertexDecorator<R, T> : IVertex<R, T>, IComparable<IVertexDecorator<R, T>>
	where R : struct, IComparable where T : IGraphData
{
	IVertex<R, T> InnerVertex { get; }
	bool Visited { get; set; }
	ISet<R> Path { get; set; }
	T Data { get; set; }
	void Update(T data, ISet<R> path);
	void Wrap(IVertex<R, T> innerVertex);
}

