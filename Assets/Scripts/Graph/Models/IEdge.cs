using System;

public interface IEdge<R, T> : IComparable<IEdge<R, T>> where R : struct, IComparable where T : IGraphData
{
	R Origin { get; }
	R Target { get; }
	T Data { get; }
}
