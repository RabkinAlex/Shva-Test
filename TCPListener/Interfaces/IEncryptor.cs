namespace TCPListener.Interfaces;

public interface IEncryptor
{
    string EncryptData(string data);
    string DecryptData(string encryptedData);
}