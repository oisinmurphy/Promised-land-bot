using DSharpPlus.SlashCommands;

namespace PromisedLandDSPBot.Modules.Language;

public class Module
{
    [SlashCommandGroup("gpt", "Language Model Access, Requires GPT-3 Auth")]
    public class Slash : ApplicationCommandModule
    {
        
    }
}