public class NullCryptographer : ICryptographer
{
    public string Key { get; set; }
    
    public string Encrypt (string text) => text;

    public string Decrypt (string text) => text;
}