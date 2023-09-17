public interface ISerializer
{
    T Deserialize<T> (string value);
    string Serialize<T> (T obj);
}