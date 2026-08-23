using System.Threading.Tasks;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Interfaces
{
    public interface IConstructionPlanner
    {
        Task<ConstructionProject> PlanConstructionAsync(string userRequest);
        ConstructionProject EnhancePlanWithResearchAsync(ConstructionProject project);
    }
}
