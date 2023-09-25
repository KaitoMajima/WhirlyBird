using System.Linq;
using Godot;

[GlobalClass]
public partial class MusicResource : Resource
{
    [Export]
    public MusicClipEntryResource[] MusicClipEntries;

    public MusicClipEntryResource GetByType (MusicClipType musicClipType) =>
        MusicClipEntries.First(x => x.MusicClipType == musicClipType);
}