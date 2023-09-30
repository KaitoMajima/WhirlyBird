using NUnit.Framework;
using NSubstitute;

namespace GameTests.Parallax;

public class MainMenuParallaxManagerModelTests
{
    class BaseMainMenuParallaxManagerModelTests
    {
        protected IMainMenuSettings MainMenuSettings { get; private set; }
        protected MainMenuParallaxManagerModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            MainMenuSettings = Substitute.For<IMainMenuSettings>();
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new MainMenuParallaxManagerModel(MainMenuSettings);
        }
    }

    class ParallaxOffset : BaseMainMenuParallaxManagerModelTests
    {
        [Test]
        public void Get_Settings_Parallax_Base_Value_As_Offset ()
        {
            const float PARALLAX_BASE_VALUE = 77;
            MainMenuSettings.ParallaxBaseValue.Returns(PARALLAX_BASE_VALUE);
            
            Assert.AreEqual(PARALLAX_BASE_VALUE, Model.ParallaxOffset);
        }
    }
}