public interface IVertexFactory<R, T> where R : struct where T : IGraphData
{
	IVertex<R, T> Create(R id);
}
