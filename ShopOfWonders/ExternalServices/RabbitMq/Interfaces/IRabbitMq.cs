namespace SOW.ShopOfWonders.ExternalServices.RabbitMq
{
    public interface IRabbitMq
    {
        void SendObject(object obj);

        void SendMessage(string message);
    }
}
