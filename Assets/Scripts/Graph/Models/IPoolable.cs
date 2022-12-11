public interface IPoolable
{
	bool IsAvailable { get; }
	void Reset();
}
