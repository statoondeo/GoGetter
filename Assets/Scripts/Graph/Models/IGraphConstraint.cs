public interface IGraphConstraint<T> where T : IGraphData
{
	bool IsConstrainted(T graphData);
}
