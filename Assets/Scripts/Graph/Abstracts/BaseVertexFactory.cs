public abstract class BaseVertexFactory<R, T> : IVertexFactory<R, T> where R : struct where T : IGraphData
{
	public abstract IVertex<R, T> Create(R id);
}
