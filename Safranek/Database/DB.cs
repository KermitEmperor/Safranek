
using System.Reflection;
using Microsoft.Data.Sqlite;
using Safranek.Commands;
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

    public void TableRegistration() {
        var tables = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetInterface(nameof(ITableBase)) is not null && !t.IsAbstract && !t.IsInterface);

        foreach (var tableType in tables) {
            tableType.GetMethod("Init")?.Invoke(null, null);
            Console.WriteLine($"Table named {typeof(Guilds).GetProperty("Name")!.GetValue(tableType)} from {tableType.Name} has been instantiated!");
        }
    }
}
