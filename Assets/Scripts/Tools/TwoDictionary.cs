using System.Collections.Generic;

public class TwoDictionary<T, U>
{
	protected IDictionary<T, U> ForwardIndexer;
	protected IDictionary<U, T> BackwardIndexer;

	public TwoDictionary()
	{
		ForwardIndexer = new Dictionary<T, U>();
		BackwardIndexer = new Dictionary<U, T>();
	}
	public int Count => ForwardIndexer.Count;
	public bool ContainsForwardKey(T forwardIndex) => ForwardIndexer.ContainsKey(forwardIndex);
	public bool ContainsBackwardKey(U backwardIdex) => BackwardIndexer.ContainsKey(backwardIdex);
	public U GetForwardValue(T forwardIndex) => ForwardIndexer[forwardIndex];
	public T GetBackwardValue(U backwardIdex) => BackwardIndexer[backwardIdex];
	public void Add(T forwardIndex, U backwardIdex)
	{
		if (ForwardIndexer.ContainsKey(forwardIndex))
		{
			ForwardIndexer[forwardIndex] = backwardIdex;
			BackwardIndexer[backwardIdex] = forwardIndex;
			return;
		}
		ForwardIndexer.Add(forwardIndex, backwardIdex);
		BackwardIndexer.Add(backwardIdex, forwardIndex);
	}
	public void RemoveForward(T forwardIndex)
	{
		if (!ForwardIndexer.ContainsKey(forwardIndex)) return;
		U backwardIdex = ForwardIndexer[forwardIndex];
		ForwardIndexer.Remove(forwardIndex);
		BackwardIndexer.Remove(backwardIdex);
	}
	public void RemoveBackward(U backwardIdex)
	{
		if (!BackwardIndexer.ContainsKey(backwardIdex)) return;
		T forwardIndex = BackwardIndexer[backwardIdex];
		BackwardIndexer.Remove(backwardIdex);
		ForwardIndexer.Remove(forwardIndex);
	}
}
