using Microsoft.Data.Sqlite;
using Safranek.Commands;

namespace Safranek.Database.Types;

public class Guilds : ITableBase {
    public static string Name => "guilds";

    public static void Init() {
        SqliteCommand command = Db.Instance.GetConnection().CreateCommand();
        command.CommandText = $"CREATE TABLE IF NOT EXISTS {Name} (id INTEGER PRIMARY KEY);";
        command.ExecuteNonQuery();
    }

    public static void AddServer(ulong serverId) {
        SqliteCommand command = Db.Instance.GetConnection().CreateCommand();
        command.CommandText = $"INSERT INTO {Name} VALUES (@id);";
        command.Parameters.AddWithValue("@id", serverId);
        command.ExecuteNonQuery();
    }

    public static void RemoveServer(ulong serverId) {
        SqliteCommand command = Db.Instance.GetConnection().CreateCommand();
        command.CommandText = $"DELETE FROM {Name} WHERE id = @id;";
        command.Parameters.AddWithValue("@id", serverId);
        command.ExecuteNonQuery();
    }
}
