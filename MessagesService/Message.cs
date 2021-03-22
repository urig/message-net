// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.ComponentModel.DataAnnotations;

namespace MessagesService
{
    public class Message
    {
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Content { get; set; }

        public Message()
        {
            Recipient = string.Empty;
            Sender = string.Empty;
            Content = string.Empty;
        }
    }
}
