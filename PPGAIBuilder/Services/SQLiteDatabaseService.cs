using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Services
{
    public class SQLiteDatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly string _dbPath;

        public SQLiteDatabaseService(string dbPath = "ppgaibuilder.db")
        {
            _dbPath = dbPath;
            _connectionString = $"Data Source={dbPath};Version=3;";
        }

        public async Task InitializeAsync()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();

                // Projects table
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Projects (
                        ProjectId TEXT PRIMARY KEY,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        CreatedAt TEXT NOT NULL,
                        UpdatedAt TEXT NOT NULL,
                        Data TEXT NOT NULL,
                        ThumbnailPath TEXT
                    )";
                await command.ExecuteNonQueryAsync();

                // Chat Messages table
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS ChatMessages (
                        Id TEXT PRIMARY KEY,
                        ProjectId TEXT NOT NULL,
                        Content TEXT NOT NULL,
                        Role TEXT NOT NULL,
                        Timestamp TEXT NOT NULL,
                        RelatedProjectId TEXT,
                        FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId)
                    )";
                await command.ExecuteNonQueryAsync();

                // Game Assets table
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS GameAssets (
                        AssetId TEXT PRIMARY KEY,
                        DisplayName TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Source TEXT NOT NULL,
                        PreviewImage TEXT,
                        ModelPath TEXT,
                        Metadata TEXT,
                        Description TEXT
                    )";
                await command.ExecuteNonQueryAsync();

                // Research Results cache
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS ResearchCache (
                        Id TEXT PRIMARY KEY,
                        Title TEXT NOT NULL,
                        Source TEXT NOT NULL,
                        Summary TEXT NOT NULL,
                        Content TEXT,
                        RetrievedAt TEXT NOT NULL,
                        ReliabilityScore REAL NOT NULL,
                        IsMock INTEGER NOT NULL
                    )";
                await command.ExecuteNonQueryAsync();

                await connection.CloseAsync();
            }
        }

        public async Task<T?> GetAsync<T>(string id) where T : class
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                var tableName = GetTableName<T>();

                command.CommandText = $"SELECT Data FROM {tableName} WHERE {tableName}Id = @id";
                command.Parameters.AddWithValue("@id", id);

                var result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return JsonSerializer.Deserialize<T>(result.ToString());
                }

                await connection.CloseAsync();
            }
            return null;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            var results = new List<T>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                var tableName = GetTableName<T>();

                command.CommandText = $"SELECT Data FROM {tableName}";
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var json = reader.GetString(0);
                    var obj = JsonSerializer.Deserialize<T>(json);
                    if (obj != null)
                        results.Add(obj);
                }

                await connection.CloseAsync();
            }
            return results;
        }

        public async Task SaveAsync<T>(T entity) where T : class
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                var tableName = GetTableName<T>();
                var json = JsonSerializer.Serialize(entity);

                if (entity is ConstructionProject project)
                {
                    command.CommandText = $@"
                        INSERT OR REPLACE INTO {tableName} (ProjectId, Name, Description, CreatedAt, UpdatedAt, Data, ThumbnailPath)
                        VALUES (@id, @name, @desc, @created, @updated, @data, @thumb)";
                    command.Parameters.AddWithValue("@id", project.ProjectId);
                    command.Parameters.AddWithValue("@name", project.Name);
                    command.Parameters.AddWithValue("@desc", project.Description ?? "");
                    command.Parameters.AddWithValue("@created", project.CreatedAt.ToString("O"));
                    command.Parameters.AddWithValue("@updated", project.UpdatedAt.ToString("O"));
                    command.Parameters.AddWithValue("@data", json);
                    command.Parameters.AddWithValue("@thumb", project.ThumbnailPath ?? "");
                }
                else if (entity is ChatMessage message)
                {
                    command.CommandText = $@"
                        INSERT OR REPLACE INTO {tableName} (Id, ProjectId, Content, Role, Timestamp, RelatedProjectId)
                        VALUES (@id, @projectId, @content, @role, @timestamp, @relatedProjectId)";
                    command.Parameters.AddWithValue("@id", message.Id);
                    command.Parameters.AddWithValue("@projectId", message.ProjectId);
                    command.Parameters.AddWithValue("@content", message.Content);
                    command.Parameters.AddWithValue("@role", message.Role);
                    command.Parameters.AddWithValue("@timestamp", message.Timestamp.ToString("O"));
                    command.Parameters.AddWithValue("@relatedProjectId", message.RelatedProjectId ?? "");
                }
                else if (entity is GameAsset asset)
                {
                    command.CommandText = $@"
                        INSERT OR REPLACE INTO {tableName} (AssetId, DisplayName, Category, Source, PreviewImage, ModelPath, Metadata, Description)
                        VALUES (@id, @name, @category, @source, @preview, @model, @metadata, @desc)";
                    command.Parameters.AddWithValue("@id", asset.AssetId);
                    command.Parameters.AddWithValue("@name", asset.DisplayName);
                    command.Parameters.AddWithValue("@category", asset.Category);
                    command.Parameters.AddWithValue("@source", asset.Source);
                    command.Parameters.AddWithValue("@preview", asset.PreviewImage ?? "");
                    command.Parameters.AddWithValue("@model", asset.ModelPath ?? "");
                    command.Parameters.AddWithValue("@metadata", asset.Metadata ?? "");
                    command.Parameters.AddWithValue("@desc", asset.Description);
                }

                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task DeleteAsync<T>(string id) where T : class
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                var tableName = GetTableName<T>();

                command.CommandText = $"DELETE FROM {tableName} WHERE {tableName}Id = @id";
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task<int> ExecuteAsync(string query, params object[] parameters)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = query;

                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.AddWithValue($"@param{i}", parameters[i]);
                }

                var result = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                return result;
            }
        }

        private string GetTableName<T>() where T : class
        {
            if (typeof(T) == typeof(ConstructionProject))
                return "Projects";
            if (typeof(T) == typeof(ChatMessage))
                return "ChatMessages";
            if (typeof(T) == typeof(GameAsset))
                return "GameAssets";
            if (typeof(T) == typeof(ResearchResult))
                return "ResearchCache";

            return typeof(T).Name + "s";
        }
    }
}
