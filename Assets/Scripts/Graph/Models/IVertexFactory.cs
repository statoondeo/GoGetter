public interface IVertexFactory<T> where T : class, IGraphData
{
	IVertex<T> Create(int id);
}