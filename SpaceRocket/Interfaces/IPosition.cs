using System;

namespace SpaceRocket.Domain.Interfaces
{
    public interface IPosition
    {
        int X { get; }
        int Y { get; }

        //static ISize Create(int x, int y) => throw new NotImplementedException();
    }
}
