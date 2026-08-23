using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IDatabaseService _db;

        public ProjectService(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<ConstructionProject> CreateProjectAsync(string name, string description)
        {
            var project = new ConstructionProject
            {
                Name = name,
                Description = description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _db.SaveAsync(project);
            return project;
        }

        public async Task<ConstructionProject?> GetProjectAsync(string projectId)
        {
            return await _db.GetAsync<ConstructionProject>(projectId);
        }

        public async Task<List<ConstructionProject>> GetAllProjectsAsync()
        {
            return await _db.GetAllAsync<ConstructionProject>();
        }

        public async Task SaveProjectAsync(ConstructionProject project)
        {
            project.UpdatedAt = DateTime.UtcNow;
            await _db.SaveAsync(project);
        }

        public async Task DeleteProjectAsync(string projectId)
        {
            await _db.DeleteAsync<ConstructionProject>(projectId);
        }

        public async Task<List<ChatMessage>> GetProjectConversationAsync(string projectId)
        {
            var allMessages = await _db.GetAllAsync<ChatMessage>();
            var filtered = new List<ChatMessage>();

            foreach (var msg in allMessages)
            {
                if (msg.ProjectId == projectId)
                    filtered.Add(msg);
            }

            return filtered;
        }

        public async Task SaveChatMessageAsync(ChatMessage message)
        {
            await _db.SaveAsync(message);
        }
    }
}
