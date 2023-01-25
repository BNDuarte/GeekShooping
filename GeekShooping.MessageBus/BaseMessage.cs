namespace GeekShooping.MessageBus
{
    public class BaseMessage
    {
        public long id { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}