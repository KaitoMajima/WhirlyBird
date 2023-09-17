using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TapNFlap.Core.Saving.Cryptography;

public class AESCryptographer : ICryptographer
{
    const int IV_BYTE_COUNT = 16;
    
    public string Key { get; set; }

    public string Encrypt (string text)
    {
        byte[] byteKey = ConvertKey(Key);
        using Aes aes = CreateNewAes(byteKey);

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using MemoryStream memoryStream = new();
        memoryStream.Write(aes.IV, 0, IV_BYTE_COUNT);

        using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
        using StreamWriter streamWriter = new(cryptoStream);
        streamWriter.Write(text);
        streamWriter.Close();

        byte[] convertedBytes = memoryStream.ToArray();
        return Convert.ToBase64String(convertedBytes);
    }

    public string Decrypt (string text)
    {
        byte[] byteKey = ConvertKey(Key);
        byte[] convertedBytes = Convert.FromBase64String(text);
        byte[] storedIv = new byte[IV_BYTE_COUNT];
            
        using MemoryStream memoryStream = new(convertedBytes);
        memoryStream.Read(storedIv, 0, IV_BYTE_COUNT);

        using Aes aes = CreateNewAes(byteKey, storedIv);

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, storedIv);

        using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);
        string decryptedText = streamReader.ReadToEnd();
        streamReader.Close();
            
        return decryptedText;
    }

    static Aes CreateNewAes (byte[] key = null, byte[] iv = null)
    {
        Aes aes = Aes.Create();
        if (key != null)
            aes.Key = key;
        if (iv != null)
            aes.IV = iv;
        return aes;
    }
    
    static byte[] ConvertKey (string key)
    {
        if (key == null)
            throw new InvalidOperationException(
                "Cryptography key is null when using cryptography."
            );

        byte[] byteKey = Encoding.UTF8.GetBytes(key);
        return byteKey;
    }
}