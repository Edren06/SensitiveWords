using Microsoft.AspNetCore.Mvc;
using SecretWords.WebApp.Models;

/// <summary>
/// The admin controller for all the sensitive word crud operations
/// </summary>
public class AdminController : Controller
{
    private readonly ApiService _api;

    public AdminController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index()
    {
        var words = await _api.GetAllWordsAsync();
        return View(words);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return BadRequest("Word is required.");

        var created = await _api.CreateWordAsync(word);
        if (created == null)
            return BadRequest("Failed to create word.");

        return Json(new { success = true, word = created });
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SensitiveWordViewModel model)
    {
        if (!string.IsNullOrWhiteSpace(model.Word))
        {
            await _api.UpdateWordAsync(model);
            return Json(new { success = true });
        }
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteWordAsync(id);
        return Json(new { success = true });
    }
}
