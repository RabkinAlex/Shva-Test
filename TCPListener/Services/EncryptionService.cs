using System.Security.Cryptography;
using System.Text;
using TCPListener.Interfaces;

namespace TCPListener.Services;

public class EncryptionService : IEncryptor
{
    private readonly byte[] _encryptionKey;
    private readonly byte[] _encryptIv;
    public EncryptionService()
    {
        _encryptionKey = GenerateRandomKey();
        _encryptIv = GenerateRandomIV();
    }

    private byte[] GenerateRandomKey()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateKey();

            return aesAlg.Key;
        }
    }

    private byte[] GenerateRandomIV()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateIV();

            return aesAlg.IV;
        }
    }
    public string EncryptData(string data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = _encryptionKey;
            aes.IV = _encryptIv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public string DecryptData(string encryptedData)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = _encryptionKey;
            aes.IV = _encryptIv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

            return decryptedText;
        }
    }
}