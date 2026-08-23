using System.Threading.Tasks;

namespace PPGAIBuilder.Interfaces
{
    public class SafetyResult
    {
        public bool Allowed { get; set; }
        public string Reason { get; set; } = "";
        public string Category { get; set; } = "Uncategorized";
    }

    public interface ISafetyService
    {
        Task<SafetyResult> CheckInputAsync(string input);
        Task<SafetyResult> CheckOutputAsync(string output);
        bool ContainsPromptInjectionAttempt(string input);
    }
}
