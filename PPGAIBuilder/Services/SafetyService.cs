using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;

namespace PPGAIBuilder.Services
{
    public class SafetyService : ISafetyService
    {
        private readonly List<string> _weaponKeywords = new()
        {
            "gun", "rifle", "pistol", "missile", "bomb", "explosive", "TNT", "grenade",
            "launcher", "cannon", "mine", "warhead", "nuke", "nuclear"
        };

        private readonly List<string> _harmKeywords = new()
        {
            "torture", "harm", "hurt", "kill", "murder", "poison", "toxic", "dangerous drug",
            "chemical weapon", "biological weapon", "weaponized"
        };

        private readonly List<string> _injectionPatterns = new()
        {
            "ignore previous",
            "disable safety",
            "developer mode",
            "bypass",
            "reveal system",
            "show prompt",
            "system prompt",
            "jailbreak",
            "override",
            "remove restrictions"
        };

        public Task<SafetyResult> CheckInputAsync(string input)
        {
            if (ContainsPromptInjectionAttempt(input))
            {
                return Task.FromResult(new SafetyResult
                {
                    Allowed = false,
                    Reason = "Request contains prompt injection or jailbreak attempt.",
                    Category = "Injection Attack"
                });
            }

            var lowerInput = input.ToLower();

            if (_weaponKeywords.Any(kw => lowerInput.Contains(kw)))
            {
                return Task.FromResult(new SafetyResult
                {
                    Allowed = false,
                    Reason = "Request involves weapons or explosives. This is only allowed in the context of People Playground gameplay.",
                    Category = "Weapons"
                });
            }

            if (_harmKeywords.Any(kw => lowerInput.Contains(kw)))
            {
                return Task.FromResult(new SafetyResult
                {
                    Allowed = false,
                    Reason = "Request involves serious harm. This application is for People Playground game construction only.",
                    Category = "Harm"
                });
            }

            return Task.FromResult(new SafetyResult
            {
                Allowed = true,
                Reason = "Request passed safety checks.",
                Category = "Safe"
            });
        }

        public Task<SafetyResult> CheckOutputAsync(string output)
        {
            // Similar checks for output
            return CheckInputAsync(output);
        }

        public bool ContainsPromptInjectionAttempt(string input)
        {
            var lowerInput = input.ToLower();
            return _injectionPatterns.Any(pattern => lowerInput.Contains(pattern));
        }
    }
}
