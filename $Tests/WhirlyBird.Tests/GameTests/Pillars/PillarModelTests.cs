using System;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.Pillars;

public class PillarModelTests
{
    class BasePillarModelTests
    {
        protected ITimer Timer { get; private set; }
        protected PillarModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            Timer = Substitute.For<ITimer>();
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new PillarModel();
            Model.SetTimer(Timer);
        }
    }

    class Initialize : BasePillarModelTests
    {
        [Test]
        public void Mark_Pillar_For_Destruction_After_Interval ()
        {
            const int SECONDS_UNTIL_DESTRUCTION = 7;
            bool hasPillarDestructionEventTriggered = false;
            Model.OnPillarMarkedForDestruction += () => hasPillarDestructionEventTriggered = true;
            
            Model.Setup(SECONDS_UNTIL_DESTRUCTION);
            Model.Initialize();

            Timer.Timeout += Raise.Event<Action>();
            
            Assert.IsTrue(hasPillarDestructionEventTriggered);
        }
    }
}