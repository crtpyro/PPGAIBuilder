using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Interfaces
{
    public interface IAIService
    {
        Task<string> ProcessUserRequestAsync(string userInput, string projectContext);
        Task<ConstructionProject> GenerateConstructionPlanAsync(string userRequest);
        bool IsAvailable { get; }
        string ProviderName { get; }
    }
}
