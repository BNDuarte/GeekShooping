using GeekShooping.PaymentAPI.Messages;
using GeekShooping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShooping.PaymentAPI.MessageConsume
{
    public class RabbirMQPaymentConsumer : BackgroundService
    {
        public IConnection _connection;
        private IModel _chanel;
        private readonly IProcessPayment _processPayment;

        public RabbirMQPaymentConsumer(IProcessPayment processPayment)
        {
            _processPayment = processPayment;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();

            _chanel = _connection.CreateModel();
            _chanel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consume = new EventingBasicConsumer(_chanel);
            consume.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage vo = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessPayment(vo).GetAwaiter().GetResult();
                _chanel.BasicAck(evt.DeliveryTag, false);
            };
            _chanel.BasicConsume("checkoutqueue", false, consume);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {
            try
            {
                //_rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}
