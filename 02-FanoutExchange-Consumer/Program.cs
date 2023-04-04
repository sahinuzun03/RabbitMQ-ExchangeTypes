//Consumer Console App
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
ConnectionFactory factory = new();
factory.Uri = new("amqps://agynxcdk:czCiBq-QnX_MgyDat8Iqxl2bVtBOGZFH@woodpecker.rmq.cloudamqp.com/agynxcdk");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "fanout-exchange-example", ExchangeType.Fanout);

Console.WriteLine("Kuyruk Adını Giriniz");
string queueName = Console.ReadLine();

channel.QueueDeclare(queue: queueName, exclusive: false);

//Bind mekanizması exchange ile kuyruk arasındaki ilişkilendirilmesi birbirlerine bağlanması işlemi
channel.QueueBind(
    queue:queueName,
    exchange: "fanout-exchange-example",
    routingKey:string.Empty
    );

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: queueName,
    autoAck:true,
    consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();