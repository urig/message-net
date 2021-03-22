using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MessagesService
{
    public interface IMessagesRepository
    {
        IEnumerable<Message> GetMessages(string recipient);
        void AddMessage(string recipient, Message message);
    }

    public class MessagesRepository : IMessagesRepository
    {
        private readonly ConcurrentDictionary<string, IList<Message>> _store;

        public MessagesRepository()
        {
            _store = new ConcurrentDictionary<string, IList<Message>>();
        }

        public IEnumerable<Message> GetMessages(string recipient)
        {
            ValidateRecipient(recipient);
            if (_store.TryGetValue(recipient, out var messages))
            {
                return messages;
            }
            return new List<Message>();
        }

        public void AddMessage(string recipient, Message message)
        {
            ValidateRecipient(recipient);
            message.Recipient = recipient;
            _store.AddOrUpdate(
                key:
                    message.Recipient,
                addValue: // new recipient =  put message in new list
                    new List<Message> { message },
                updateValueFactory: // existing recipient = add message to existing list
                    (_, messages) =>
                        {
                            messages.Add(message);
                            return messages;
                        });
        }

        private static void ValidateRecipient(string recipient)
        {
            if (string.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentException($"{nameof(recipient)} cannot be null or empty");
            }
        }
    }
}
