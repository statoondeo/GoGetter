﻿public abstract class BaseGraphData : IGraphData
{
	protected BaseGraphData() { }
	public abstract IGraphData Add(params IGraphData[] data);
	public virtual int CompareTo(IGraphData other) => 0;
	public virtual void Reset() { }
}