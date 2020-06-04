using SpaceRocket.Domain.Interfaces;

namespace SpaceRocket.Domain.Aggregates
{
    public class Size : ISize
    {
        public int X { get; }
        public int Y { get; }

        protected Size(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Size Create(int x, int y)
        {
            var size = new Size(x, y);
            return size;
        }
    }
}
