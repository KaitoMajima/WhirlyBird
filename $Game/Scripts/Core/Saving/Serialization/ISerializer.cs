namespace TapNFlap.Core.Saving.Serialization;

public interface ISerializer
{
    T Deserialize<T> (string value);
    string Serialize<T> (T obj);
}