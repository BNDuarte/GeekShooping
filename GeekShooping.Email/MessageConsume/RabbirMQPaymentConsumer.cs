using GeekShooping.Email.Messages;
using GeekShooping.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShooping.Email.MessageConsume
{
    public class RabbirMQPaymentConsumer : BackgroundService
    {
        private readonly EmailRepository _repository;
        public IConnection _connection;
        private IModel _chanel;
        private const string _exchange = "FanoutPaumentUpdateExchange";
        private  string _queueName = "";
        public RabbirMQPaymentConsumer(EmailRepository repository)
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
                UpdatePaymentResultMessage message = JsonSerializer.Deserialize<UpdatePaymentResultMessage>(content);
                ProcessLogs(message).GetAwaiter().GetResult();
                _chanel.BasicAck(evt.DeliveryTag, false);
            };
            _chanel.BasicConsume(_queueName, false, consume);
            return Task.CompletedTask;
        }

        private async Task ProcessLogs(UpdatePaymentResultMessage message)
        {
            try
            {
                await _repository.LogEmail(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
