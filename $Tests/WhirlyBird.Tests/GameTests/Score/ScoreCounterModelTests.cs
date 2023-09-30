using System;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.Score;

public class ScoreCounterModelTests
{
    class BaseScoreCounterModelTests
    {
        protected IScoreData ScoreData { get; private set; }
        protected IMainGameSavingSystem GameSavingSystem { get; private set; }
        protected IPlayerModel PlayerModel { get; private set; }
        protected IGameOverModel GameOverModel { get; private set; }
        
        protected ScoreCounterModel Model { get; private set; }
            
        [SetUp]
        public void Setup ()
        {
            ScoreData = Substitute.For<IScoreData>();
            GameSavingSystem = Substitute.For<IMainGameSavingSystem>();
            PlayerModel = Substitute.For<IPlayerModel>();
            GameOverModel = Substitute.For<IGameOverModel>();
            
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new ScoreCounterModel(
                ScoreData, 
                GameSavingSystem,
                PlayerModel,
                GameOverModel
            );
        }
    }
    
    class PlayerScore : BaseScoreCounterModelTests
    {
        [Test]
        public void Increases_Score_Data_On_Player_Score ()
        {
            bool scoreDetectedEventTriggered = false;
            Model.OnScoreDetected += () => scoreDetectedEventTriggered = true;
            
            Model.Initialize();
            PlayerModel.OnPlayerScored += Raise.Event<Action>();
            
            Assert.AreEqual(1, Model.Score);
            Assert.AreEqual(1, Model.Highscore);
            GameSavingSystem.Received().Save();
            Assert.IsTrue(scoreDetectedEventTriggered);
        }
        
        [Test]
        public void Doesnt_Increase_Score_Data_If_GameOver_Triggered ()
        {
            bool scoreDetectedEventTriggered = false;
            Model.OnScoreDetected += () => scoreDetectedEventTriggered = true;
            
            Model.Initialize();
            GameOverModel.OnGameOverTriggered += Raise.Event<Action>();
            PlayerModel.OnPlayerScored += Raise.Event<Action>();
            
            Assert.AreEqual(0, Model.Score);
            Assert.AreEqual(0, Model.Highscore);
            GameSavingSystem.DidNotReceive().Save();
            Assert.IsFalse(scoreDetectedEventTriggered);
        }
        
        [Test]
        public void Doesnt_Change_Highscore_Data_If_Score_Is_Lower ()
        {
            const int HIGHSCORE = 10;
            ScoreData.Highscore = HIGHSCORE;
            
            Model.Initialize();
            PlayerModel.OnPlayerScored += Raise.Event<Action>();
            
            Assert.AreEqual(1, Model.Score);
            Assert.AreEqual(HIGHSCORE, Model.Highscore);
        }
    }
}