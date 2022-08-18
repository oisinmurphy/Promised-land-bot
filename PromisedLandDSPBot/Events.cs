using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using PromisedLandDSPBot.Handlers;

namespace PromisedLandDSPBot;

public class Events
{
    internal static async Task CommandsOnCommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
    {
        Console.WriteLine($"\nNormal Command Error: \n{e.Exception.ToString()}\n\n and {e.Context.Command} was the culprit.");
        await OnErrorChannelReport(e.Context.Member, e.Context.Channel, e.Exception);
        //throw new NotImplementedException();
    }

    internal static async Task SlashOnSlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        Console.WriteLine($"Slash Command Error: \n{e.Exception.ToString()}\n\n and {e.Context.CommandName} was the culprit.");
        await OnErrorChannelReport(e.Context.Member, e.Context.Channel, e.Exception);
        //throw new NotImplementedException();
    }
    
    
    //@Jerry, I dont know what the fuck this is... but I'm going to let you fix it...
    
    /// <summary>
    /// To report back to the user regarding the error that occured. We do not tell what happened, just the fact that it did.
    /// </summary>
    /// <param name="m">The Guild Member Invoker</param>
    /// <param name="dc">The Channel to post this message in. </param>
    /// <returns></returns>
    private static async Task OnErrorChannelReport(DiscordMember? m, DiscordChannel dc, Exception e)
    {
        if (m == null) return;

        switch (e)
        {
            case CommandNotFoundException:
                await dc.SendMessageAsync(
                    $"{m.Mention} sorry but that command is not recognised");
                break;
            case InvalidOperationException:
                await dc.SendMessageAsync(
                    $"{m.Mention} the command you entered was invalid");
                break;
            default:
                await dc.SendMessageAsync(
                    $"sorry {m.Mention}, an error occured while trying to execute your command");
                break;
        }
    }
    
    internal static Task GuildDiscovered(DiscordClient sender, GuildCreateEventArgs e)
    {
        //sets the bots current nickname to that of the applications name whenever a new guild is loaded (such as on-join or application restart)
        
        /*
        e.Guild.GetMemberAsync(sender.CurrentUser.Id).Result
            .ModifyAsync(model => model.Nickname = sender.CurrentApplication.Name);
        */
        
        //checks if the guild is registered in config.json and notifies the user.
        Console.WriteLine(Config.Whitelist.Get().Result.Contains(e.Guild.Id)
            ? $"[ENFORCER] {e.Guild.Name} [{e.Guild.Id}] [SUCCESS]"
            : $"[ENFORCER] {e.Guild.Name} [{e.Guild.Id}] [FAILED]");
        return Task.CompletedTask;
    }

    public static Task ModalSubmitted(DiscordClient sender, ModalSubmitEventArgs e)
    {
        Modal.Handle(sender, e);
        return Task.CompletedTask;
    }
}