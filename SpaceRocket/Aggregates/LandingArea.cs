using SpaceRocket.Core.Abstracts;
using SpaceRocket.Domain.Exceptions;
using SpaceRocket.Domain.Interfaces;
using System;
using System.Linq;

namespace SpaceRocket.Domain.Aggregates
{
    public class LandingArea : Entity
    {
        private LandingPlatform _landingPlatform;

        public LandingPlatform LandingPlatform { get { return _landingPlatform; } }

        public ISize Size { get; private set; }

        private void SetSize(ISize landingAreaSize)
        {
            if (landingAreaSize == null)
                throw new ArgumentNullException(nameof(landingAreaSize));

            Size = Aggregates.Size.Create(landingAreaSize.X, landingAreaSize.Y);
        }

        private bool IsValidatePlatformPosition(LandingPlatform landingPlatform)
        {
            IPosition position = landingPlatform.PlatformPosition;
            var cols = Enumerable.Range(1, Size.X).ToList();
            var rows = Enumerable.Range(1, Size.Y).ToList();
            
            if (!cols.Contains(position.X) || !rows.Contains(position.Y))
            {
                throw new OutOfLandingAreaDomainException();
            }

            //Checking if the platform is inside of the landing area
            int maxPlatformCols = Enumerable.Range(position.X, landingPlatform.Size.X).Max();
            int maxPlatformRows = Enumerable.Range(position.Y, landingPlatform.Size.Y).Max();
            if (!cols.Contains(maxPlatformCols) || !rows.Contains(maxPlatformRows))
            {
                throw new OutOfLandingAreaDomainException();
            }

            return true;            
        }

        public static LandingArea Create(ISize landingAreaSize)
        { 
            LandingArea landingArea = new LandingArea();
            landingArea.SetSize(landingAreaSize);
            return landingArea;
        }

        public void AddLandingPlatform(LandingPlatform landingPlatform)
        {
            if (landingPlatform == null)
                throw new ArgumentNullException(nameof(landingPlatform));

            if (IsValidatePlatformPosition(landingPlatform))
            {
                _landingPlatform = landingPlatform;
            }            
        }
    }
}
