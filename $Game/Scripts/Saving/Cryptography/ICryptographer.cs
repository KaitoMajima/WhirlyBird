public interface ICryptographer
{
    string Key { get; set; }
    string Encrypt (string text);
    string Decrypt (string text);
}