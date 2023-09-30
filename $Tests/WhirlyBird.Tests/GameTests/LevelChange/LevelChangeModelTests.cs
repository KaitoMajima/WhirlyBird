using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.LevelChange;

public class LevelChangeModelTests
{
    class BaseLevelChangeModelTests
    {
        protected ILevelChangeSettings LevelChangeSettings { get; private set; }
        protected IPillarManagerModel PillarManagerModel { get; private set; }

        protected LevelChangeModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            LevelChangeSettings = Substitute.For<ILevelChangeSettings>();
            PillarManagerModel = Substitute.For<IPillarManagerModel>();
            
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new LevelChangeModel(
                LevelChangeSettings,
                PillarManagerModel
            );
        }
        
        protected void SetupLevelChanges ()
        {
            ILevelChangeUniqueSettings levelChange1 =
                Substitute.For<ILevelChangeUniqueSettings>();
                
            ILevelChangeUniqueSettings levelChange2 =
                Substitute.For<ILevelChangeUniqueSettings>();

            IReadOnlyList<ILevelChangeUniqueSettings> pillarDifficulties = new List<ILevelChangeUniqueSettings>
            {
                levelChange1, 
                levelChange2
            };

            LevelChangeSettings.LevelChanges.Returns(pillarDifficulties);
        }
    }

    class HandlePillarPassed : BaseLevelChangeModelTests
    {
        [Test]
        public void Level_Change_Detected ()
        {
            const int LEVEL_CHANGE_REQUIREMENT = 77;
            const int EXPECTED_LEVEL_CHANGE_ID = 1;
            bool hasLevelChangeEventTriggered = false;

            SetupLevelChanges();
                
            ILevelChangeUniqueSettings nextLevelChange = LevelChangeSettings.LevelChanges[1];
            nextLevelChange.Id.Returns(EXPECTED_LEVEL_CHANGE_ID);
            nextLevelChange.PillarsPassedRequirement.Returns(LEVEL_CHANGE_REQUIREMENT);

            PillarManagerModel.PillarsPassedCount.Returns(LEVEL_CHANGE_REQUIREMENT);
            Model.OnLevelChanged += () => hasLevelChangeEventTriggered = true;
            
                
            Model.Initialize();
            PillarManagerModel.OnPillarPassed += Raise.Event<Action>();
                
            Assert.AreEqual(EXPECTED_LEVEL_CHANGE_ID, Model.CurrentLevelId);
            Assert.IsTrue(hasLevelChangeEventTriggered);
        }
    }
}