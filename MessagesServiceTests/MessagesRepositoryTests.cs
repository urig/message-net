using System;
using System.Linq;
using MessagesService;
using Shouldly;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration

namespace MessagesServiceTests
{
    public class MessagesRepositoryTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void GetMessages_BadRecipient_Throws(string recipient)
        {
            var target = new MessagesRepository();
            Should.Throw<ArgumentException>(() =>
                target.GetMessages(recipient));
        }

        [Fact]
        public void GetMessages_NoMessages_EmptyList()
        {
            var target = new MessagesRepository();
            target.GetMessages("foo").ShouldBeEmpty();
        }

        [Fact]
        public void AddMessage_OneMessageOneRecipient_IsListed()
        {
            // Arrange
            var target = new MessagesRepository();
            var message = new Message { Recipient = "foo", Sender = "bar", Content = "Hello, World!" };
            // Assert
            target.AddMessage("foo", message);
            // Assert
            var actual = target.GetMessages("foo");
            actual.ShouldContain(message);
            actual.ShouldHaveSingleItem();
        }

        [Fact]
        public void AddMessage_TwoMessagesOneRecipient_AreListed()
        {
            // Arrange
            var target = new MessagesRepository();
            var message1 = new Message { Recipient = "foo", Sender = "bar", Content = "Hello, World!" };
            var message2 = new Message { Recipient = "foo", Sender = "bar", Content = "Hello, World!" };
            // Act
            target.AddMessage("foo", message1);
            target.AddMessage("foo", message2);
            // Assert
            var actual = target.GetMessages("foo");
            actual.ShouldContain(message1);
            actual.ShouldContain(message2);
        }

        [Fact]
        public void AddMessage_TwoMessagesTwoRecipients_AreListed()
        {
            // Arrange
            var target = new MessagesRepository();
            var message1 = new Message { Recipient = "foo", Sender = "bar", Content = "Hello, World!" };
            var message2 = new Message { Recipient = "baz", Sender = "bar", Content = "Hello, World!" };
            // Act
            target.AddMessage("foo", message1);
            target.AddMessage("baz", message2);
            // Assert
            target.GetMessages("foo").ShouldContain(message1);
            target.GetMessages("baz").ShouldContain(message2);
        }

        [Fact]
        public void AddMessage_RecipientDifferentFromMessage_MessageRecipientIsOverwritten()
        {
            // Arrange
            var target = new MessagesRepository();
            var message = new Message { Recipient = "foo", Sender = "bar", Content = "Hello, World!" };
            // Assert
            target.AddMessage("baz", message);
            // Assert
            var actual = target.GetMessages("baz");
            actual.Single().Recipient.ShouldBe("baz");
        }
    }
}
