public sealed class GoGetterGraph : Graph
{
	private const int MAX_EDGE = 24;
	public GoGetterGraph() : base(MAX_EDGE)
	{
		for (int i = 0; i < MAX_EDGE; i++) AddVertex(i + 1);
	}
}