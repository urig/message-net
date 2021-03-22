using System;
using System.Collections.Generic;

namespace MessagesService
{
    public interface IMessagesRepository
    {
        IEnumerable<Message> GetMessages(string recipient);
    }

    public class MessagesRepository : IMessagesRepository
    {
        private readonly IDictionary<string, IEnumerable<Message>> _messages;

        public MessagesRepository()
        {
            _messages = new Dictionary<string, IEnumerable<Message>>();
        }

        public IEnumerable<Message> GetMessages(string recipient)
        {
            if (string.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentException($"{nameof(recipient)} cannot be null or empty");
            }
            if (_messages.TryGetValue(recipient, out var messages))
            {
                return messages;
            }
            return new List<Message>();
        }
    }
}
