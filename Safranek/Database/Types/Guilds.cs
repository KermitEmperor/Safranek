using Microsoft.Data.Sqlite;
using Safranek.Commands;

namespace Safranek.Database.Types;

public class Guilds : ITableBase {
    public static string Name => "guilds";

    public static void Init() {
        SqliteCommand command = DB.Instance.getConnection().CreateCommand();
        command.CommandText = $"CREATE TABLE IF NOT EXISTS {Name} (id INTEGER PRIMARY KEY);";
        command.ExecuteNonQuery();
    }

    public static void addServer(ulong serverId) {
        SqliteCommand command = DB.Instance.getConnection().CreateCommand();
        command.CommandText = $"INSERT INTO {Name} VALUES (@id);";
        command.Parameters.AddWithValue("@id", serverId);
        command.ExecuteNonQuery();
    }

    public static void removeServer(ulong serverId) {
        SqliteCommand command = DB.Instance.getConnection().CreateCommand();
        command.CommandText = $"DELETE FROM {Name} WHERE id = @id;";
        command.Parameters.AddWithValue("@id", serverId);
        command.ExecuteNonQuery();
    }
}
