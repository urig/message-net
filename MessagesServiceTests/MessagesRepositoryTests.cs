using System;
using MessagesService;
using Shouldly;
using Xunit;

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
    }
}
