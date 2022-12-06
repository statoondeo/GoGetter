public sealed class EdgeFactory : BaseEdgeFactory<int, GraphData>
{
	public override IEdge<int, GraphData> Create(int originId, int targetId, GraphData data)
		=> new Edge(originId, targetId, data);
}