public class PoolManager : BasePoolManager<VertexDecorator>
{
	public PoolManager(int size, IPoolableFactory itemsFactory) : base(size, itemsFactory) { }
}
