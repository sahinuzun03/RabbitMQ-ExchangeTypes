//Consumer Console App
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
ConnectionFactory factory = new();
factory.Uri = new("amqps://agynxcdk:czCiBq-QnX_MgyDat8Iqxl2bVtBOGZFH@woodpecker.rmq.cloudamqp.com/agynxcdk");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//1.Adım : 
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);


//2.Adım
string queueName = channel.QueueDeclare().QueueName;

//3.Adım
channel.QueueBind(
    queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();



//1.Adım : Publisher'da ki exchange ile biebir aynı isim ve type2asahip bir exchange tanımlanmalıdır!
//2.Adım : Publisher tarafından routingKey'de bulunan değerdeki kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemi gerekmektedir.Bunun için öncelikle bir uyruk oluşturulmalıdır.

//3.Adım : 