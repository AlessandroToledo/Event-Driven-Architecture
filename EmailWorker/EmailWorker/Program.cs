using EmailWorker.Entities;
using StackExchange.Redis;
using System.Text.Json;

class Program
{
    private const string RedisConnectionString = "localhost:6379";
    private static readonly ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);
    private const string OFFENSIVE_CHANNEL = "offensive-channel";
    private const string LOG_CHANNEL = "log-channel";
    static void Main(string[] args)
    {
        Console.WriteLine("Aguardando evento para envio de E-mail...");
        var pubsub = connection.GetSubscriber();

        pubsub.SubscribeAsync(OFFENSIVE_CHANNEL, async (channel, message) =>
        {
            Console.WriteLine("\nEvento idetificado, iniciando envio de E-mail...");
            Console.WriteLine("E-mail enviado com sucesso!");
            Message messageLog = JsonSerializer.Deserialize<Message>(message)!;
            messageLog.Text = "E-mail notificando usuário "+messageLog.User+" pela infração foi enviado com sucesso!";
            messageLog.User = "E-mail Notifier";
            var json = JsonSerializer.Serialize(messageLog);
            await pubsub.PublishAsync(LOG_CHANNEL, json);

        });
        Console.ReadLine();
    }
}
