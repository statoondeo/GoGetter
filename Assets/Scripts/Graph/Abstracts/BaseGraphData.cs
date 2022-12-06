public abstract class BaseGraphData : IGraphData
{
	protected BaseGraphData() { }
	public abstract IGraphData Add(IGraphData data);
	public virtual int CompareTo(IGraphData other) => 0;
}