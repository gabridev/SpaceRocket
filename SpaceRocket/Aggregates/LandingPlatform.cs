using SpaceRocket.Domain.Interfaces;

namespace SpaceRocket.Domain.Aggregates
{
    public class LandingPlatform
    {
        public Size Size { get; private set; }

        public static LandingPlatform Create(ISize size)
        {
            LandingPlatform landingPlatform = new LandingPlatform();
            landingPlatform.SetSize(size);
            return landingPlatform;
        }

        public void SetSize(ISize size)
        {
            Size = Size.Create(size.X, size.Y);
        }
    }
}
