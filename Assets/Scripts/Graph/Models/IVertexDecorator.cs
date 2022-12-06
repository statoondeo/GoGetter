using System.Collections.Generic;

public interface IVertexDecorator<R, T> : IVertex<R, T> where R : struct where T : IGraphData
{
	IVertex<R, T> InnerVertex { get; }
	bool Visited { get; }
	bool EverPerformed { get; set; }
	IList<R> Path { get; set; }
	T Data { get; set; }
	IList<R> Visit(IGraphConstraint<T> constraint);
}