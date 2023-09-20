using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record PlayerSettings (
    [property: JsonProperty] float PlayerSize,
    [property: JsonProperty] float PlayerGravityScale,
    [property: JsonProperty] float PlayerJumpStrength
)
: IPlayerSettings;