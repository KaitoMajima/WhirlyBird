using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.Pillars;

public class PillarManagerModelTests
{
    class BasePillarManagerModelTests
    {
        protected IPillarSpawnSettings PillarSpawnSettings { get; private set; }
        protected IRandomProvider RandomProvider { get; private set; }
        protected IScoreCounterModel ScoreCounterModel { get; private set; }
        protected ITimer Timer { get; private set; }
        
        protected PillarManagerModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            PillarSpawnSettings = Substitute.For<IPillarSpawnSettings>();
            RandomProvider = Substitute.For<IRandomProvider>();
            ScoreCounterModel = Substitute.For<IScoreCounterModel>();
            Timer = Substitute.For<ITimer>();
            
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new PillarManagerModel(PillarSpawnSettings, RandomProvider);
            Model.Setup(ScoreCounterModel);
            Model.SetTimer(Timer);
        }

        protected void SetupPillarDifficulties ()
        {
            IPillarSettings difficulty1 =
                Substitute.For<IPillarSettings>();
                
            IPillarSettings difficulty2 =
                Substitute.For<IPillarSettings>();

            IReadOnlyList<IPillarSettings> pillarDifficulties = new List<IPillarSettings>
            {
                difficulty1, 
                difficulty2
            };

            PillarSpawnSettings.PillarDifficulty.Returns(pillarDifficulties);
        }
    }

    class Initialize : BasePillarManagerModelTests
    {
        [Test]
        public void Pillar_Spawned_Successfully ()
        {
            const int PILLAR_SPAWN_INTERVAL = 7;

            SetupPillarDifficulties();
            IPillarSettings firstPillarDifficulty = PillarSpawnSettings.PillarDifficulty[0];
            firstPillarDifficulty.PillarSpawnInterval.Returns(PILLAR_SPAWN_INTERVAL);
            
            bool hasEventTriggered = false;
            Model.OnPillarSpawn += () => hasEventTriggered = true;
                
            Model.Initialize();
            Model.StartSpawning();
            Timer.Timeout += Raise.Event<Action>();
                
            Assert.IsTrue(hasEventTriggered);
        }
    }

    class GetRandomSpawningPoint : BasePillarManagerModelTests
    {
        [Test]
        public void Return_Correct_Spawning_Point ()
        {
            const float MIN_SPAWN_POINT = 7;
            const float SPAWN_POINT = 77;
            const float MAX_SPAWN_POINT = 777;
            
            PillarSpawnSettings.PillarSpawnMinYHeight.Returns(MIN_SPAWN_POINT);
            PillarSpawnSettings.PillarSpawnMaxYHeight.Returns(MAX_SPAWN_POINT);
            RandomProvider.Range(PillarSpawnSettings.PillarSpawnMinYHeight,
                PillarSpawnSettings.PillarSpawnMaxYHeight).Returns(SPAWN_POINT);

            float spawningPointValue = Model.GetNewRandomSpawningPoint();
                
            Assert.AreEqual(SPAWN_POINT, spawningPointValue);
        }
    }
}