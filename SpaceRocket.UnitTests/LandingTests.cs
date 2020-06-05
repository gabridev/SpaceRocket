using SpaceRocket.Domain.Aggregates;
using SpaceRocket.Domain.Enums;
using SpaceRocket.Domain.Exceptions;
using SpaceRocket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace SpaceRocket.UnitTests
{
    public class LandingTests
    {

        [Fact]
        public void Create_landing_area_success()
        {
            ISize landingAreaSize = Size.Create(100, 100);
            ISize platformSize = Size.Create(10, 10);
            IPosition platformoutPosition = Position.Create(5, 5);           

            Assert.NotNull(Landings.Create(landingAreaSize, platformSize, platformoutPosition));            
        }

        [Fact]
        public void Create_landing_area_error()
        {
            ISize landingAreaSize = Size.Create(100, 100);
            ISize platformSize = Size.Create(10, 10);
            IPosition platformoutPosition = Position.Create(101, 5);
            IPosition platformPosition = Position.Create(95, 95);

            Assert.Throws<OutOfLandingAreaDomainException>(() => Landings.Create(landingAreaSize, platformSize, platformoutPosition));
            Assert.Throws<OutOfLandingAreaDomainException>(() => Landings.Create(landingAreaSize, platformSize, platformPosition));
        }


        [Fact]
        public void Rocket_landing_success()
        {
            Landings landings = Landings.Default();

            LandingResponseEnum result = landings.Land(5, 5);
           
            Assert.Equal(LandingResponseEnum.OkForLanding, result);
        }

        [Fact]
        public void Rocket_landing_out_platform()
        {
            Landings landings = Landings.Default();
           
            LandingResponseEnum result = landings.Land(16, 15);
            
            Assert.Equal(LandingResponseEnum.OutOfPlatform, result);
        }

        [Fact]
        public void Rocket_landing_clash()
        {
            Landings landings = Landings.Default();
           
            landings.Land(7, 7);

            LandingResponseEnum clash1 = landings.Land(7, 8);
            LandingResponseEnum clash2 = landings.Land(6, 7);
            LandingResponseEnum clash3 = landings.Land(6, 6);

            Assert.Equal(LandingResponseEnum.Clash, clash1);
            Assert.Equal(LandingResponseEnum.Clash, clash2);
            Assert.Equal(LandingResponseEnum.Clash, clash3);
        }

        [Fact]
        public void Multiple_rocket_landing_success()
        {
            Landings landings = Landings.Default();

            IPosition rocket1 = Position.Create(5, 5);
            IPosition rocket2 = Position.Create(7, 7);

            List<LandingResponseEnum> results = landings.Land(new List<IPosition>{ rocket1, rocket2} );

            Assert.True(results.TrueForAll(r=> r == LandingResponseEnum.OkForLanding));
        }

        [Fact]
        public void Multiple_rocket_landing_clash()
        {
            Landings landings = Landings.Default();

            IPosition rocket1 = Position.Create(7, 7);
            IPosition rocket2 = Position.Create(7, 8);

            List<LandingResponseEnum> results = landings.Land(new List<IPosition> { rocket1, rocket2 });

            Assert.True(results.FindAll(r => r == LandingResponseEnum.Clash).Count > 0);
        }

        
    }
}
