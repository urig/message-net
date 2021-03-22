// ReSharper disable UnusedAutoPropertyAccessor.Global

using System;
using System.ComponentModel.DataAnnotations;

namespace MessagesService
{
    public class Message
    {
        public Guid Id { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Content { get; set; }

        public Message()
        {
            Id = Guid.Empty;
            Recipient = string.Empty;
            Sender = string.Empty;
            Content = string.Empty;
        }
    }
}
