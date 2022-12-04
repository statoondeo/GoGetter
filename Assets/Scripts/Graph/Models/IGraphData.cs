using System;

public interface IGraphData : IComparable<IGraphData>
{
	IGraphData Add(params IGraphData[] data);
	void Reset();
}
