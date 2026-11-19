namespace BudgetManager.Web.Services.AI
{
    public interface ILanguageModelService
    {
        Task<string> GenerateResponseAsync(string prompt, IReadOnlyList<ChatMessage> history, CancellationToken cancellationToken = default);
    }
}
