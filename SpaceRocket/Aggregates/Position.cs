using SpaceRocket.Domain.Interfaces;
using System;

namespace SpaceRocket.Domain.Aggregates
{
    public class Position : IPosition
    {
        public int X { get; }
        public int Y { get; }

        protected Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position Create(int x, int y)
        {
            var size = new Position(x, y);
            return size;
        }
       
    }
}
