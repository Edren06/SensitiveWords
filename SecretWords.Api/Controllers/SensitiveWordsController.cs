using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Api.Interfaces;
using SensitiveWords.Api.Models;
using SensitiveWords.Api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SensitiveWords.Api.Controllers.Internal
{
    /// <summary>
    /// Controller for basic CRUD operations of sensitive words
    /// </summary>
    [ApiController]
    [Route("api/sensitivewords")]
    public class SensitiveWordsController : ControllerBase
    {
        private readonly ISensitiveWordService _wordService;
        public SensitiveWordsController(ISensitiveWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieve all sensitive words",
            Description = "This call retrieves all the sensitive words that exist in the database."
        )]
        [SwaggerResponse(200, "Returns the list of sensitive words", typeof(IEnumerable<SensitiveWord>))]
        public async Task<IActionResult> GetAll()
        {
            var items = await _wordService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Get a sensitive word by it's ID",
            Description = "Retrieves a single sensitive word by its ID."
        )]
        [SwaggerResponse(200, "Found the sensitive word", typeof(SensitiveWord))]
        [SwaggerResponse(404, "Sensitive word not found")]
        public async Task<IActionResult> Get(int id)
        {
            var items = await _wordService.GetAllAsync();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null) 
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new sensitive word",
            Description = "Adds a new word to the sensitive words list."
        )]
        [SwaggerResponse(201, "Sensitive word created successfully", typeof(SensitiveWord))]
        [SwaggerResponse(400, "Invalid input")]
        public async Task<IActionResult> Create(SensitiveWord s)
        {
            if (string.IsNullOrWhiteSpace(s.Word)) 
                return BadRequest("Word required");

            var created = await _wordService.CreateAsync(s.Word);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Update a sensitive word",
            Description = "Updates an existing sensitive word by its ID."
        )]
        [SwaggerResponse(200, "Sensitive word updated successfully", typeof(SensitiveWord))]
        [SwaggerResponse(400, "ID mismatch or invalid input")]
        [SwaggerResponse(404, "Sensitive word not found")]
        public async Task<IActionResult> Update(int id, [FromBody] SensitiveWord dto)
        {
            if (id != dto.Id) 
                return BadRequest();

            var updated = await _wordService.UpdateAsync(dto);
            if (updated == null) 
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a sensitive word",
            Description = "Deletes a sensitive word by its ID."
        )]
        [SwaggerResponse(204, "Sensitive word deleted successfully")]
        [SwaggerResponse(404, "Sensitive word not found")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _wordService.DeleteAsync(id);
            
            if (!ok) 
                return NotFound();

            return NoContent();
        }
    }
}
