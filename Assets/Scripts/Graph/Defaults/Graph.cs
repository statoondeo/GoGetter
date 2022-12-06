public class Graph : BaseGraph<int, GraphData>
{
	public Graph() : base(new VertexFactory(), new VertexDecoratorFactory(), new EdgeFactory(), new NoGraphConstraint<GraphData>()) { }
}
