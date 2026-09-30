namespace Safranek;

class Program {
    public static async Task Main(string[] args) {
        
        Client client = new(Environment.GetEnvironmentVariable("TOKEN")!);
        
        await client.Start();
    }
}
