using BudgetManager.Web.Services.AI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace BudgetManager.Web.Controllers
{
    [Authorize]
    public class BudgetAssistantController : Controller
    {
        private readonly ILanguageModelService _languageModel;
        private const int MaxMessages = 20;
        public BudgetAssistantController(ILanguageModelService languageModel)
        {
            _languageModel = languageModel;
        }

        private string SessionKey
        {
            get
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Authenticated user ID not found.");

                return $"BudgetAssistant_{userId}";
            }
        }

        private List<ChatMessage> GetChatHistory()
        {
            var json = HttpContext.Session.GetString(SessionKey);

            if (string.IsNullOrEmpty(json))
            {
                return new List<ChatMessage>();
            }

            return JsonSerializer.Deserialize<List<ChatMessage>>(json) ?? new List<ChatMessage>();
        }

        private void SaveHistory(List<ChatMessage> history)
        {
            var recentMessages = history.TakeLast(MaxMessages).ToList();

            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(recentMessages));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetChatHistory());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string prompt, CancellationToken cancellationToken)
        {
            var history = GetChatHistory();

            if (string.IsNullOrWhiteSpace(prompt) || prompt.Length > 2000)
            {
                ModelState.AddModelError(nameof(prompt), "Enter a question of up to 2,000 characters.");

                return View(history);
            }

            //ViewBag.Prompt = prompt;

            try
            {
                //ViewBag.Response = await _languageModel.GenerateResponseAsync(prompt, cancellationToken);
                var response = await _languageModel.GenerateResponseAsync(prompt, history, cancellationToken);

                history.Add(new ChatMessage { Role = "user", Text = prompt });
                history.Add(new ChatMessage { Role = "assistant", Text = response });

                SaveHistory(history);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                ModelState.AddModelError(string.Empty, "Gemini took too long to respond.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
                //ModelState.AddModelError(string.Empty, $"Gemini Error: {ex.Message}.");
            }
            return View(history);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(SessionKey);

            return RedirectToAction(nameof(Index));
        }
    }
}
