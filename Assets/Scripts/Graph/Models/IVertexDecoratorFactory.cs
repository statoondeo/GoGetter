using System;

public interface IVertexDecoratorFactory<R, T> : IPoolableFactory where R : struct, IComparable where T : IGraphData
{
}
