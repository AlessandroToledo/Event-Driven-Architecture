// See https://aka.ms/new-console-template for more information
using LogWorker.Entities;
using StackExchange.Redis;
using System.Text.Json;

class Program
{
    private const string RedisConnectionString = "localhost:6379";
    private static readonly ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(RedisConnectionString);
    private const string Channel = "log-channel";

    static void Main(string[] args)
    {
        Console.WriteLine("Listando Logs ChatApp");
        var pubsub = connection.GetSubscriber();

        pubsub.SubscribeAsync(Channel, (channel, message) =>
        {
            Message messageLog = JsonSerializer.Deserialize<Message>(message)!;
            if(messageLog.User == "E-mail Notifier")
            {
                Console.WriteLine("\nLog-Channel: Envio de E-mail \nUsuário: {0} \nMensagem: \"{1}\" \nData: \"{2}\"", messageLog.User, messageLog.Text, DateTime.UtcNow.AddHours(-3));
            }
            else
            {
                if (messageLog.Offensive)
                {
                    Console.WriteLine("\nLog-Channel: Mensagem Censurada \nUsuário: {0} \nMensagem: \"Detectamos o uso de linguagem imprópria na mensagem.\" \nData: \"{1}\"", messageLog.User, DateTime.UtcNow.AddHours(-3));
                }
                else
                {
                    Console.WriteLine("\nLog-Channel: Nova Mensagem \nUsuário: {0} \nMensagem: \"{1}\" \nData: \"{2}\"", messageLog.User, messageLog.Text, DateTime.UtcNow.AddHours(-3));
                }
            }
            
        });
        Console.ReadLine();
    }
}
