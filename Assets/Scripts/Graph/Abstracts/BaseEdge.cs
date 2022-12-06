using System;

public abstract class BaseEdge<R, T> : IEdge<R, T> where R : struct where T : IGraphData
{
	public R Origin { get; protected set; }
	public R Target { get; protected set; }
	public T Data { get; protected set; }

	protected BaseEdge(R originId, R targetId, T data)	{
		Data = data ?? throw new ArgumentNullException(nameof(data));
		Origin = originId;
		Target = targetId;
	}
}
