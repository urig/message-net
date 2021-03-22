using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessagesService
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRepository _repository;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessagesRepository repository, ILogger<MessagesController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [HttpGet("{recipient}")]
        public IActionResult Get(string recipient)
        {
            try
            {
                var messages = _repository.GetMessages(recipient);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in MessagesController.Get(). recipient: [{recipient}]", ex);
                return StatusCode(500);
            }
        }

        [HttpPost("{recipient}")]
        public IActionResult Post(string recipient, Message message)
        {
            try
            {
                _repository.AddMessage(recipient, message);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in MessagesController.Post(). message: [{message}]", ex);
                return StatusCode(500);
            }

        }
    }
}
