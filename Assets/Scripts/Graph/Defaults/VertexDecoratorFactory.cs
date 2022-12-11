public class VertexDecoratorFactory : BaseVertexDecoratorFactory<int, GraphData>
{
	public override IPoolable Create() => new VertexDecorator();
}