using System.Diagnostics.CodeAnalysis;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.SlashCommands;
using OpenAI;
using PromisedLandDSPBot.Functions.Permissions;
namespace PromisedLandDSPBot.Modules.Language;
/// <summary>
/// This Module defines moderation functionality through the bot. (bans, kicks, etc.)
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Module
{
    [SlashCommandGroup("gpt", "Language Model Module"), RequireUserPermissions(Permissions.Administrator), RequireGuild()]
    public class Slash : ApplicationCommandModule
    {
        OpenAIAPI api = new OpenAIAPI(apiKeys: "", engine: Engine.Davinci);
        
        [SlashCommand("request", "repeats the provided phrase")]
        [RequireGuild]
        [RequirePermissions(Permissions.SendMessages)]
        public async Task Request(InteractionContext ctx,
            [Option("prompt", "the message you would like the AI to respond to")] string prompt, [Option("tokens", "how many tokens would you like to allocate to your request")] long tokens)
        {
            await ctx.CreateResponseAsync("sorry, this module has been disabled.", true);
            /*
            if (tokens > 100 && !Developer.IsTeamMember(ctx).Result)
            {
                await ctx.CreateResponseAsync("sorry, but only app developers and owners are allowed to use more than 100 tickets in a single request.", true);
            }
            
            var request = new CompletionRequestBuilder()
                    .WithPrompt(prompt)
                    .WithMaxTokens((int)tokens)
                    .Build();

            await Task.Delay(800);
            
            var result = api.Completions.CreateCompletionAsync(request).Result;

            //await ctx.DeleteResponseAsync();

            await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention} asked `{prompt.Replace("`", string.Empty)}`" + result);
            */
        }
    }
}