using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Interfaces
{
    public interface IProjectService
    {
        Task<ConstructionProject> CreateProjectAsync(string name, string description);
        Task<ConstructionProject?> GetProjectAsync(string projectId);
        Task<List<ConstructionProject>> GetAllProjectsAsync();
        Task SaveProjectAsync(ConstructionProject project);
        Task DeleteProjectAsync(string projectId);
        Task<List<ChatMessage>> GetProjectConversationAsync(string projectId);
        Task SaveChatMessageAsync(ChatMessage message);
    }
}
