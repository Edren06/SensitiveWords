using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller used for the user to enter in a word to be 'blooped'
/// </summary>
public class ChatController : Controller
{
    private readonly ApiService _api;

    public ChatController(ApiService api)
    {
        _api = api;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Send(string message)
    {
        var bloopedMessage = await _api.BloopMessageAsync(message);
        ViewBag.BloopedMessage = bloopedMessage;
        return View("Index");
    }
}
