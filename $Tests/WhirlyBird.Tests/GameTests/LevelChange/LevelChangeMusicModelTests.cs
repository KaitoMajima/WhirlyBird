using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

namespace GameTests.LevelChange;

public class LevelChangeMusicModelTests
{
    class BaseLevelChangeMusicModelTests
    {
        protected ILevelChangeModel LevelChangeModel { get; private set; }
        protected IMusicManagerSystem MusicManagerSystem { get; private set; }

        protected LevelChangeMusicModel Model { get; private set; }

        [SetUp]
        public void Setup ()
        {
            LevelChangeModel = Substitute.For<ILevelChangeModel>();
            MusicManagerSystem = Substitute.For<IMusicManagerSystem>();
            
            CreateModel();
        }

        protected void CreateModel ()
        {
            Model = new LevelChangeMusicModel(
                LevelChangeModel,
                MusicManagerSystem
            );
        }
    }

    class Initialize : BaseLevelChangeMusicModelTests
    {
        [Test]
        public void Change_Music_For_Level_0_On_Initialize ()
        {
            const int LEVEL = 0;
            const MusicClipType CLIP_TYPE = MusicClipType.Level0;
            
            LevelChangeModel.CurrentLevelId.Returns(LEVEL);
            Model.Initialize();
            
            MusicManagerSystem.Received().Play(CLIP_TYPE);
        }
        
        [Test]
        public void Change_Music_For_Level_1_On_Initialize ()
        {
            const int LEVEL = 1;
            const MusicClipType CLIP_TYPE = MusicClipType.Level1;
            
            LevelChangeModel.CurrentLevelId.Returns(LEVEL);
            Model.Initialize();
            
            MusicManagerSystem.Received().Play(CLIP_TYPE);
        }
    }

    class HandleLevelChanged : BaseLevelChangeMusicModelTests
    {
        [Test]
        public void Change_Music_For_Level_0_On_LevelChanged ()
        {
            const int LEVEL = 0;
            const MusicClipType CLIP_TYPE = MusicClipType.Level0;
            
            Model.Initialize();
            LevelChangeModel.CurrentLevelId.Returns(LEVEL);
            LevelChangeModel.OnLevelChanged += Raise.Event<Action>();
            
            MusicManagerSystem.Received().Play(CLIP_TYPE);
        }
        
        [Test]
        public void Change_Music_For_Level_1_On_LevelChanged ()
        {
            const int LEVEL = 1;
            const MusicClipType CLIP_TYPE = MusicClipType.Level1;
            
            Model.Initialize();
            LevelChangeModel.CurrentLevelId.Returns(LEVEL);
            LevelChangeModel.OnLevelChanged += Raise.Event<Action>();
            
            MusicManagerSystem.Received().Play(CLIP_TYPE);
        }
    }
}