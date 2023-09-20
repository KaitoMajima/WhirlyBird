using Newtonsoft.Json;

public class JsonSerializer : ISerializer
{
    public T Deserialize<T> (string value) 
        => JsonConvert.DeserializeObject<T>(value);

    public string Serialize<T> (T obj) 
        => JsonConvert.SerializeObject(obj);
}