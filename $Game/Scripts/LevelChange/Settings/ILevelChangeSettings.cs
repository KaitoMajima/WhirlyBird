using System.Collections.Generic;

public interface ILevelChangeSettings
{
    IReadOnlyList<ILevelChangeUniqueSettings> LevelChanges { get; }
}