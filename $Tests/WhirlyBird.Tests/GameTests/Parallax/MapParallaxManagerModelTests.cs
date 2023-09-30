using System;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.Parallax;

public class MapParallaxManagerModelTests
{
    class BaseMapParallaxManagerModelTests
    {
        protected IPillarManagerModel PillarManagerModel { get; private set; }
        protected MapParallaxManagerModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            PillarManagerModel = Substitute.For<IPillarManagerModel>();
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new MapParallaxManagerModel(PillarManagerModel);
        }
    }

    class Initialize : BaseMapParallaxManagerModelTests
    {
        [Test]
        public void Get_Calculated_Offset_On_Initialization ()
        {
            const float PARALLAX_BASE_VALUE = 77;
            const float PARALLAX_MULTIPLIER = 7;
            
            PillarManagerModel.ParallaxBaseValue.Returns(PARALLAX_BASE_VALUE);
            PillarManagerModel.ParallaxMultiplier.Returns(PARALLAX_MULTIPLIER);
            
            Model.Initialize();
            
            Assert.AreEqual(PARALLAX_BASE_VALUE * PARALLAX_MULTIPLIER, Model.ParallaxOffset);
        }
    }
    
    class HandleDifficultyChanged : BaseMapParallaxManagerModelTests
    {
        [Test]
        public void Get_Calculated_Offset_On_Pillar_Difficulty_Change ()
        {
            Model.Initialize();
            
            const float PARALLAX_BASE_VALUE = 77;
            const float PARALLAX_MULTIPLIER = 7;
            
            PillarManagerModel.ParallaxBaseValue.Returns(PARALLAX_BASE_VALUE);
            PillarManagerModel.ParallaxMultiplier.Returns(PARALLAX_MULTIPLIER);

            PillarManagerModel.OnPillarDifficultyChanged += Raise.Event<Action>();
            
            Assert.AreEqual(PARALLAX_BASE_VALUE * PARALLAX_MULTIPLIER, Model.ParallaxOffset);
        }
    }
}