using System;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.GameOver;

public class GameOverModelTests
{
    class BaseGameOverModelTests
    {
        protected IPlayerModel PlayerModel { get; private set; }
        protected GameOverModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            PlayerModel = Substitute.For<IPlayerModel>();
            
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new GameOverModel(PlayerModel);
        }
    }

    class HandlePlayerDamaged : BaseGameOverModelTests
    {
        [Test]
        public void Set_GameOver_To_True ()
        {
            bool hasGameOverEventTriggered = false;
            Model.OnGameOverTriggered += () => hasGameOverEventTriggered = true;
            
            Model.Intialiize();
            PlayerModel.OnPlayerDamaged += Raise.Event<Action>();
            
            PlayerModel.Received().Kill();
            Assert.IsTrue(hasGameOverEventTriggered);
        }
    }
}