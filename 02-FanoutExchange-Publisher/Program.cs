//Consumer Console App
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
ConnectionFactory factory = new();
factory.Uri = new("amqps://agynxcdk:czCiBq-QnX_MgyDat8Iqxl2bVtBOGZFH@woodpecker.rmq.cloudamqp.com/agynxcdk");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare("fanout-exchange-example", ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(
        exchange: "fanout-exchange-example", 
        routingKey: string.Empty, 
        body: message);
}

Console.Read();