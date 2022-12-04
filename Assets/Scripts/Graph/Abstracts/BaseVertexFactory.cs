public abstract class BaseVertexFactory<T> : IVertexFactory<T> where T : class, IGraphData, new()
{
	public abstract IVertex<T> Create(int id);
}