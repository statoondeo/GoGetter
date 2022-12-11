using System.Diagnostics.CodeAnalysis;

public class GraphData : BaseGraphData
{
	public int Cost { get; protected set; }
	public GraphData() : this(int.MaxValue) { }
	public GraphData(int cost) => Cost = cost;
	public override IGraphData Add(IGraphData data) => new GraphData(Cost + (data is null ? 0 : ((GraphData)data).Cost));
	public override int CompareTo([AllowNull] IGraphData other) => other is null ? Cost.CompareTo(null) : Cost.CompareTo(((GraphData)other).Cost);
	public override void Clear() => Cost = 0;
}