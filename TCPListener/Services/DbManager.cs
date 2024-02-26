using TCPListener.Interfaces;
using TCPListener.Persistence;
using TCPListener.Persistence.Entities;

namespace TCPListener.Services;

public class DbManager : IDbManager
{
    private readonly EncDataDbContext _encDataDbContext;

    public DbManager(EncDataDbContext encDataDbContext)
    {
        _encDataDbContext = encDataDbContext;
    }

    public async Task AddEncryptedData(string data)
    {
        try
        {
            var entity = new TestTableEntity { EncData = data, CreatedAt = DateTime.UtcNow };
            var res = _encDataDbContext.TestTable.Add(entity);
            await _encDataDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }
}