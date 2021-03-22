using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MessagesService.Controllers
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
                _logger.LogWarning($"Exception in MessagesController.Get(). recipient: [{recipient}]", ex);
                return StatusCode(500);
            }
        }
    }
}
