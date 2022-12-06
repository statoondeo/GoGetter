public sealed class GoGetterGraph : Graph
{
	public GoGetterGraph() : base()
	{
		for (int i = 0; i < 24; i++) AddVertex(i + 1);
	}
}