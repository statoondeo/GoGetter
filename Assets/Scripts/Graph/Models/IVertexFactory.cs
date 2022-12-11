using System;

public interface IVertexFactory<R, T> where R : struct, IComparable where T : IGraphData
{
	IVertex<R, T> Create(R id);
}
