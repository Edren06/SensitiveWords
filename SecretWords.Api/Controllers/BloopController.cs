using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Api.Interfaces;
using SensitiveWords.Api.Models;
using SensitiveWords.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SensitiveWords.Api.Controllers
{
    /// <summary>
    /// Controller used for any message transformations in order to return the *'s in place of the sensitive words.
    /// </summary>
    [ApiController]
    [Route("api/bloop")]
    public class BloopController : ControllerBase
    {
        private readonly ISensitiveWordService _wordService;
        public BloopController(ISensitiveWordService wordService) 
        { 
            _wordService = wordService; 
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Send through the message to be 'blooped'",
            Description = "Sending through the message will check if a sensitive word is within the database and then return a blooped message back"
        )]
        [SwaggerResponse(200, "Message amended", typeof(BloopResponse))]
        public async Task<ActionResult<BloopResponse>> Post([FromBody] BloopRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
                return BadRequest("message required");

            var amended = await _wordService.BloopAsync(request.Message);
            return Ok(new BloopResponse { Message = amended });
        }
    }
}
