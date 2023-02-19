using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOW.ShopOfWonders.ExternalServices.RabbitMq;

namespace SOW.ShopOfWonders.Controllers.RabbitMq
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RabbitMqController : ControllerBase
    {
        private IRabbitMq _rabbitMqService;

        public RabbitMqController(IRabbitMq rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            _rabbitMqService.SendMessage(message);

            return Ok("Сообщение отправлено");
        }
    }
}
