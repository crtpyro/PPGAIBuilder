using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Services
{
    public class ConstructionPlanner : IConstructionPlanner
    {
        private readonly IAIService _aiService;
        private readonly IResearchService _researchService;
        private readonly ISafetyService _safetyService;

        public ConstructionPlanner(IAIService aiService, IResearchService researchService, ISafetyService safetyService)
        {
            _aiService = aiService;
            _researchService = researchService;
            _safetyService = safetyService;
        }

        public async Task<ConstructionProject> PlanConstructionAsync(string userRequest)
        {
            // Check safety
            var safetyResult = await _safetyService.CheckInputAsync(userRequest);
            if (!safetyResult.Allowed)
            {
                throw new InvalidOperationException($"Request blocked by safety service: {safetyResult.Reason}");
            }

            // Generate plan from AI
            var project = await _aiService.GenerateConstructionPlanAsync(userRequest);

            // Enhance with research
            var enhanced = EnhancePlanWithResearchAsync(project);

            return enhanced;
        }

        public ConstructionProject EnhancePlanWithResearchAsync(ConstructionProject project)
        {
            // This would be async in a full implementation
            // For now, just return the project as-is
            return project;
        }
    }
}
