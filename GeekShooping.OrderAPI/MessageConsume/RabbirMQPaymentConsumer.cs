using GeekShooping.OrderAPI.Messages;
using GeekShooping.OrderAPI.RabbitMQSender;
using GeekShooping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace GeekShooping.OrderAPI.MessageConsume
{
    public class RabbirMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        public IConnection _connection;
        private IModel _chanel;
        private const string _exchange = "FanoutPaumentUpdateExchange";
        private  string _queueName = "";
        public RabbirMQPaymentConsumer(OrderRepository repository, IRabbitMQMessageSender rabbitMQSender)
        {
            _repository = repository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();

            _chanel = _connection.CreateModel();
            _chanel.ExchangeDeclare(_exchange,ExchangeType.Fanout);
            _queueName = _chanel.QueueDeclare().QueueName;
            _chanel.QueueBind(_queueName,_exchange,"");

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consume = new EventingBasicConsumer(_chanel);
            consume.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultVO vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                UpdatePaymentSatus(vo).GetAwaiter().GetResult();
                _chanel.BasicAck(evt.DeliveryTag, false);
            };
            _chanel.BasicConsume(_queueName, false, consume);
            return Task.CompletedTask;
        }

        private async Task UpdatePaymentSatus(UpdatePaymentResultVO vo)
        {
            try
            {
                await _repository.UpdateOrderPaymentStatus(vo.OrderId,vo.Status);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
