using NUnit.Framework;
using NSubstitute;

namespace GameTests.Pause;

public class PauseModelTests
{
    class BasePauseModelTests
    {
        protected IGameStateProvider GameStateProvider { get; private set; }
        protected ITimeProvider TimeProvider { get; private set; }

        protected PauseModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            GameStateProvider = Substitute.For<IGameStateProvider>();
            TimeProvider = Substitute.For<ITimeProvider>();
            
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new PauseModel(
                GameStateProvider,
                TimeProvider
            );
        }

        protected void AssertPauseIsTrue (bool hasPauseEventTriggered)
        {
            Assert.IsTrue(Model.IsPaused);
            Assert.AreEqual(0, TimeProvider.TimeScale);
            Assert.IsTrue(hasPauseEventTriggered);
        }

        protected void AssertPauseIsFalse (bool hasPauseEventTriggered)
        {
            Assert.IsFalse(Model.IsPaused);
            Assert.AreEqual(1, TimeProvider.TimeScale);
            Assert.IsTrue(hasPauseEventTriggered);
        }
    }

    class SetPause : BasePauseModelTests
    {
        [Test]
        public void Pause_Set_To_True ()
        {
            bool hasPauseEventTriggered = false;
            Model.OnPauseTriggered += () => hasPauseEventTriggered = true;

            Model.SetPause(true);
            AssertPauseIsTrue(hasPauseEventTriggered);
        }

        [Test]
        public void Pause_Set_To_False ()
        {
            bool hasPauseEventTriggered = false;
            Model.OnPauseTriggered += () => hasPauseEventTriggered = true;

            Model.SetPause(false);
            AssertPauseIsFalse(hasPauseEventTriggered);
        }
    }

    class PauseToggle : BasePauseModelTests
    {
        [Test]
        public void Pause_Toggle_Set_To_True ()
        {
            bool hasPauseEventTriggered = false;
            Model.OnPauseTriggered += () => hasPauseEventTriggered = true;

            Model.PauseToggle();
            AssertPauseIsTrue(hasPauseEventTriggered);
        }

        [Test]
        public void Pause_Set_To_False ()
        {
            bool hasPauseEventTriggered = false;
            Model.OnPauseTriggered += () => hasPauseEventTriggered = true;

            Model.PauseToggle();
            Model.PauseToggle();
            AssertPauseIsFalse(hasPauseEventTriggered);
        }
    }
}