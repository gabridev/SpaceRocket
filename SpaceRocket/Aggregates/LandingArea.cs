using SpaceRocket.Domain.Interfaces;

namespace SpaceRocket.Domain.Aggregates
{
    public class LandingArea
    {
        public Size Size { get; private set; }

        public static LandingArea Create(ISize size)
        {
            LandingArea landingArea = new LandingArea();
            landingArea.SetSize(size);
            return landingArea;
        }

        public void SetSize(ISize size)
        {
            Size = Size.Create(size.X, size.Y);
        }

    }
}
