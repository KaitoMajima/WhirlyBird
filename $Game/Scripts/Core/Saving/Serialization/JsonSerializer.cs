using Newtonsoft.Json;

namespace TapNFlap.Core.Saving.Serialization;

public class JsonSerializer : ISerializer
{
    public T Deserialize<T> (string value) 
        => JsonConvert.DeserializeObject<T>(value);

    public string Serialize<T> (T obj) 
        => JsonConvert.SerializeObject(obj);
}