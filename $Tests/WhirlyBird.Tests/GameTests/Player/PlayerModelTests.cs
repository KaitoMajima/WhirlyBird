using System;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.Player;

public class PlayerModelTests
{
    class BasePlayerModelTests
    {
        protected IPlayerSettings PlayerSettings { get; private set; }
        protected ILevelChangeModel LevelChangeModel { get; private set; }
        protected PlayerModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            PlayerSettings = Substitute.For<IPlayerSettings>();
            LevelChangeModel = Substitute.For<ILevelChangeModel>();
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new PlayerModel(PlayerSettings, LevelChangeModel);
        }
    }

    class Score : BasePlayerModelTests
    {
        [Test]
        public void Sucessful_Score_Trigger ()
        {
            bool hasScoreEventTriggered = false;
            Model.OnPlayerScored += () => hasScoreEventTriggered = true;

            Model.Score();

            Assert.IsTrue(hasScoreEventTriggered);
        }
    }
        
    class Damage : BasePlayerModelTests
    {
        [Test]
        public void Sucessful_Damage_Trigger ()
        {
            bool hasDamageEventTriggered = false;
            Model.OnPlayerDamaged += () => hasDamageEventTriggered = true;

            Model.Damage();

            Assert.IsTrue(hasDamageEventTriggered);
        }
    }
        
    class Kill : BasePlayerModelTests
    {
        [Test]
        public void Sucessful_Kill_Trigger ()
        {
            bool hasKillEventTriggered = false;
            Model.OnPlayerKilled += () => hasKillEventTriggered = true;

            Model.Kill();
            
            Assert.IsTrue(hasKillEventTriggered);
        }
    }
    
    class HandlePlayerTransformed : BasePlayerModelTests
    {
        [Test]
        public void Sucessful_Transform_Trigger ()
        {
            bool hasKillEventTriggered = false;
            Model.OnPlayerTransformed += () => hasKillEventTriggered = true;

            Model.Initialize();
            LevelChangeModel.OnLevelChanged += Raise.Event<Action>();
            
            Assert.IsTrue(hasKillEventTriggered);
        }
    }
}