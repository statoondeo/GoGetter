public class Graph : BaseGraph<int, GraphData>
{
	public Graph(int maxSize) 
		: base(
			new VertexFactory(),
			new PoolManager(maxSize, new VertexDecoratorFactory()), 
			new EdgeFactory(), 
			new NoGraphConstraint<GraphData>()) { }
}
