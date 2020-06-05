using SpaceRocket.Core.Abstracts;
using SpaceRocket.Domain.Exceptions;
using SpaceRocket.Domain.Interfaces;
using System;
using System.Linq;

namespace SpaceRocket.Domain.Aggregates
{
    public class LandingPlatform : Entity
    {
        private ISize _size;
        private IPosition _platformPosition;
        private int _platformRocketSeparation;
        private IPosition _lastRocketPosition;
        
        public ISize Size { get { return _size;  } }
        public IPosition PlatformPosition { get { return _platformPosition; } }
        public IPosition LastRocketPosition { get { return _lastRocketPosition; } }
        public int PlatformRocketSeparation { get { return _platformRocketSeparation; } }

        public static LandingPlatform Create(ISize size, IPosition position, int platformRocketSeparation)
        {
            LandingPlatform landingPlatform = new LandingPlatform();
            landingPlatform.SetSize(size);
            landingPlatform.SetPlatformPosition(position);
            landingPlatform.SetPlatformRocketSeparation(platformRocketSeparation);
            return landingPlatform;
        }

        public void SetSize(ISize size)
        {
            if (size == null)
                throw new ArgumentNullException(nameof(size));

            _size = Aggregates.Size.Create(size.X, size.Y);
        }

        public void SetPlatformPosition(IPosition position)
        {
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            _platformPosition = Position.Create(position.X, position.Y);
        }

        public void SetPlatformRocketSeparation(int platformRocketSeparation)
        {
            if (platformRocketSeparation == 0)
                throw new ArgumentException(nameof(platformRocketSeparation));

            _platformRocketSeparation = platformRocketSeparation;
        }

        public void SetRocket(IPosition rocketPosition)
        {           
            if (IsValidPlatformPosition(rocketPosition))
            {
                CheckLastRocketPosition(rocketPosition);
            }

            _lastRocketPosition = rocketPosition;
        }

        private bool IsValidPlatformPosition(IPosition position)
        {
            var cols = Enumerable.Range(PlatformPosition.X, Size.X).ToList();
            var rows = Enumerable.Range(PlatformPosition.Y, Size.Y).ToList();

            if (!cols.Contains(position.X) || !rows.Contains(position.Y))
            {
                throw new OutOfPlatformDomainException();
            }
            
            return true;
        }

        private void CheckLastRocketPosition(IPosition position)
        {
            if (_lastRocketPosition != null)
            {
                if ((LastRocketPosition.X == position.X + PlatformRocketSeparation || LastRocketPosition.X == position.X - PlatformRocketSeparation)
                     || (LastRocketPosition.Y == position.Y + PlatformRocketSeparation || LastRocketPosition.Y == position.Y - PlatformRocketSeparation))
                    throw new ClashDomainException();
            }
        }
        
    }
}
