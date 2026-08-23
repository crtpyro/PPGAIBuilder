using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Services
{
    public class MockAIService : IAIService
    {
        public bool IsAvailable => true;
        public string ProviderName => "Mock AI (Local)";

        public Task<string> ProcessUserRequestAsync(string userInput, string projectContext)
        {
            var response = $"Mock AI received: '{userInput}'. " +
                          $"Context: {projectContext}. " +
                          $"This is a simulated response without requiring external API.";
            return Task.FromResult(response);
        }

        public Task<ConstructionProject> GenerateConstructionPlanAsync(string userRequest)
        {
            // Generate a sample V6 Engine project
            var project = new ConstructionProject
            {
                Name = "V6 Engine",
                Description = "A complete V6 engine assembly with crankshaft, pistons, cylinders, and connecting rods.",
                Tags = new List<string> { "Engine", "Mechanical", "Complex" }
            };

            // Create components
            var crankshaft = new Component
            {
                ComponentId = "crankshaft",
                Name = "Crankshaft",
                Category = "Engine Core",
                Description = "Main rotating shaft",
                Position = new Vector3(0, 0, 0),
                Rotation = new Vector3(0, 0, 0),
                Scale = new Vector3(1.5, 0.3, 0.3),
                Color = "#444444",
                AssetType = "Primitive"
            };

            var cylinders = new List<Component>();
            var pistons = new List<Component>();
            var rods = new List<Component>();

            // Create 6 cylinders with pistons and connecting rods
            for (int i = 0; i < 6; i++)
            {
                double xOffset = -2.5 + (i * 1.0);
                double yOffset = 1.5;

                var cylinder = new Component
                {
                    ComponentId = $"cylinder_{i}",
                    Name = $"Cylinder {i + 1}",
                    Category = "Cylinder",
                    Description = $"Cylinder chamber {i + 1}",
                    Position = new Vector3(xOffset, yOffset, 0),
                    Rotation = new Vector3(0, 0, 0),
                    Scale = new Vector3(0.3, 1.2, 0.3),
                    Color = "#666666",
                    Connections = new List<string> { "crankshaft", $"piston_{i}", $"rod_{i}" },
                    ParentId = "crankshaft",
                    AssetType = "Primitive"
                };

                var piston = new Component
                {
                    ComponentId = $"piston_{i}",
                    Name = $"Piston {i + 1}",
                    Category = "Piston",
                    Description = $"Piston for cylinder {i + 1}",
                    Position = new Vector3(xOffset, yOffset + 0.6, 0),
                    Rotation = new Vector3(0, 0, 0),
                    Scale = new Vector3(0.25, 0.3, 0.25),
                    Color = "#999999",
                    Connections = new List<string> { $"cylinder_{i}", $"rod_{i}" },
                    ParentId = $"cylinder_{i}",
                    AssetType = "Primitive"
                };

                var rod = new Component
                {
                    ComponentId = $"rod_{i}",
                    Name = $"Connecting Rod {i + 1}",
                    Category = "Rod",
                    Description = $"Connecting rod for cylinder {i + 1}",
                    Position = new Vector3(xOffset, (yOffset + 0.6 + 0) / 2, 0),
                    Rotation = new Vector3(0, 0, 45),
                    Scale = new Vector3(0.15, 0.8, 0.15),
                    Color = "#AAAAAA",
                    Connections = new List<string> { $"piston_{i}", "crankshaft" },
                    ParentId = "crankshaft",
                    AssetType = "Primitive"
                };

                cylinders.Add(cylinder);
                pistons.Add(piston);
                rods.Add(rod);
            }

            project.Components.Add(crankshaft);
            project.Components.AddRange(cylinders);
            project.Components.AddRange(pistons);
            project.Components.AddRange(rods);

            // Create construction steps
            int stepNum = 1;

            project.Steps.Add(new ConstructionStep
            {
                StepNumber = stepNum++,
                Title = "Place Crankshaft",
                Description = "Install the main crankshaft at the center of the engine. This is the core component.",
                ComponentsUsed = new List<string> { "crankshaft" },
                HighlightedComponents = new List<string> { "crankshaft" },
                TargetPosition = new Vector3(0, 0, 0),
                EstimatedDurationSeconds = 60,
                Notes = "Ensure crankshaft is aligned horizontally."
            });

            for (int i = 0; i < 6; i++)
            {
                project.Steps.Add(new ConstructionStep
                {
                    StepNumber = stepNum++,
                    Title = $"Install Cylinder {i + 1}",
                    Description = $"Attach cylinder {i + 1} to the crankshaft assembly.",
                    ComponentsUsed = new List<string> { $"cylinder_{i}" },
                    HighlightedComponents = new List<string> { $"cylinder_{i}" },
                    TargetPosition = new Vector3(-2.5 + (i * 1.0), 1.5, 0),
                    EstimatedDurationSeconds = 45,
                    Notes = $"Position cylinder {i + 1} above crankshaft."
                });

                project.Steps.Add(new ConstructionStep
                {
                    StepNumber = stepNum++,
                    Title = $"Install Piston {i + 1}",
                    Description = $"Insert piston {i + 1} into cylinder {i + 1}.",
                    ComponentsUsed = new List<string> { $"piston_{i}" },
                    HighlightedComponents = new List<string> { $"piston_{i}" },
                    TargetPosition = new Vector3(-2.5 + (i * 1.0), 1.5 + 0.6, 0),
                    EstimatedDurationSeconds = 30,
                    Notes = $"Ensure piston slides smoothly in cylinder {i + 1}."
                });

                project.Steps.Add(new ConstructionStep
                {
                    StepNumber = stepNum++,
                    Title = $"Connect Rod {i + 1}",
                    Description = $"Attach connecting rod {i + 1} between piston {i + 1} and crankshaft.",
                    ComponentsUsed = new List<string> { $"rod_{i}" },
                    HighlightedComponents = new List<string> { $"rod_{i}" },
                    TargetPosition = new Vector3(-2.5 + (i * 1.0), 1.08, 0),
                    EstimatedDurationSeconds = 40,
                    Notes = $"Rod must be angled to connect piston to crankshaft crank."
                });
            }

            project.Steps.Add(new ConstructionStep
            {
                StepNumber = stepNum++,
                Title = "Engine Assembly Complete",
                Description = "All components are now assembled. The engine is ready for testing.",
                ComponentsUsed = new List<string>(),
                HighlightedComponents = new List<string>(),
                TargetPosition = new Vector3(0, 0, 0),
                EstimatedDurationSeconds = 0,
                Notes = "Engine assembly is now complete. All pistons should move smoothly."
            });

            project.EstimatedCompletionSeconds = project.Steps.Sum(s => s.EstimatedDurationSeconds);

            return Task.FromResult(project);
        }
    }
}
