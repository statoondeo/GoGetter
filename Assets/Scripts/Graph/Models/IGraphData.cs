using System;

public interface IGraphData : IComparable<IGraphData>
{
	IGraphData Add(IGraphData data);
}
