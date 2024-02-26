namespace TCPListener.Interfaces;

public interface IDbManager
{
    Task AddEncryptedData(string data);
}