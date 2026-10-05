
using Microsoft.Data.Sqlite;
using Safranek.Database.Types;

namespace Safranek.Database;

public sealed class DB {
    private static readonly Lazy<DB> Lazy = new Lazy<DB>(() => new DB());
    public static DB Instance => Lazy.Value;
    private readonly SqliteConnection _connection;

    private DB() {
        _connection = new SqliteConnection("Data Source=safranek.db");
        _connection.Open();
    }

    public SqliteConnection getConnection() {
        return _connection;
    }
}
