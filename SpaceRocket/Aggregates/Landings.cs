using SpaceRocket.Core.Interfaces;
using SpaceRocket.Domain.Enums;
using SpaceRocket.Domain.Exceptions;
using SpaceRocket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using CoreConstants = SpaceRocket.Core.Constants;

namespace SpaceRocket.Domain.Aggregates
{
    public class Landings : IAggregate
    {
        private LandingArea _landingArea;       
        
        public LandingArea LandingArea { get { return _landingArea;  } }       
        
        protected Landings(ISize landingAreaSize, ISize platformSize, IPosition platformPosition, int platformRocketSeparation)
        {
            if (landingAreaSize == null)
                throw new ArgumentNullException(nameof(landingAreaSize));

            if (platformSize == null)
                throw new ArgumentNullException(nameof(platformSize));

            _landingArea = LandingArea.Create(landingAreaSize);

            _landingArea.AddLandingPlatform(LandingPlatform.Create(platformSize, platformPosition, platformRocketSeparation));  
        }
        
        public static Landings Default()
        {
            ISize landingAreaSize = Size.Create(CoreConstants.Landing.DefaultLandingAreaSize, CoreConstants.Landing.DefaultLandingAreaSize);
            ISize platformSize = Size.Create(CoreConstants.Landing.DefaultPlatformSize, CoreConstants.Landing.DefaultPlatformSize);
            IPosition platformPosition = Position.Create(CoreConstants.Landing.DefaultPlatformStartXPosition, CoreConstants.Landing.DefaultPlatformStartYPosition);
            Landings landing = new Landings(landingAreaSize, platformSize, platformPosition, CoreConstants.Landing.DefaultPlatformRocketSeparation);
            return landing;
        }

        public static Landings Create(ISize landingAreaSize, ISize platformSize, IPosition platformPosition)
        {           
            Landings landing = new Landings(landingAreaSize, platformSize, platformPosition, CoreConstants.Landing.DefaultPlatformRocketSeparation);
            return landing;
        }

        public LandingResponseEnum Land(int x, int y)
        {
            LandingResponseEnum landingResponse = LandingResponseEnum.Error;
            try
            {
                IPosition rocketPosition = Position.Create(x, y);
                LandingArea.LandingPlatform.SetRocket(rocketPosition);
                landingResponse = LandingResponseEnum.OkForLanding;
            }
            catch (OutOfPlatformDomainException)
            {
                landingResponse = LandingResponseEnum.OutOfPlatform;
            }
            catch (ClashDomainException)
            {
                landingResponse = LandingResponseEnum.Clash;
            }
            catch (Exception)
            {
                landingResponse = LandingResponseEnum.Error;
            }
            return landingResponse;
        }

        public List<LandingResponseEnum> Land(List<IPosition> landPositions)
        {
            List<LandingResponseEnum> landingResponses = new List<LandingResponseEnum>();
            landPositions.ForEach(rocketPosition =>
            {
                landingResponses.Add(Land(rocketPosition.X, rocketPosition.Y));
            });
            return landingResponses;
        }         
       
    }
}
