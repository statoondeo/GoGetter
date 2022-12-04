public interface IGraph<T> where T : IGraphData, new()
{
	IVertex<T> GetVertex(int id);
	IVertex<T> AddVertex(int id);
	IEdge<T> AddEdge(int idOrigin, int idTarget, T data);
	void RemoveEdge(int idOrigin, int idTarget);
	T FindPath(int origin, int target, T failResult, IGraphConstraint<T> constraint);
}