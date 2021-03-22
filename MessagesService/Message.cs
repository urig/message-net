namespace MessagesService
{
    public class Message
    {
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }

        public Message()
        {
            Recipient = string.Empty;
            Sender = string.Empty;
            Content = string.Empty;
        }

    }
}
