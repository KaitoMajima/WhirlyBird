using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public record MainMenuSettings ([property: JsonProperty] float ParallaxBaseValue) : IMainMenuSettings;