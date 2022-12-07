using System.Diagnostics.CodeAnalysis;

public sealed class GraphData : BaseGraphData
{
	public int Cost { get; private set; }
	public GraphData() : this(0) { }
	public GraphData(int cost) => Cost = cost;
	public override IGraphData Add(IGraphData data) => new GraphData(Cost + (null == data ? 0 : ((GraphData)data).Cost));
	public override int CompareTo([AllowNull] IGraphData other) => null == other ? Cost.CompareTo(null) : Cost.CompareTo(((GraphData)other).Cost);
}