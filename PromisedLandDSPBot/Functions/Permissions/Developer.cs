using DSharpPlus.SlashCommands;

namespace PromisedLandDSPBot.Functions.Permissions;

public class Developer
{
    public static async Task<bool> IsTeamMember(InteractionContext ctx)
    {
        return (ctx.Client.CurrentApplication.Owners.Any(x => x.Id == ctx.Member.Id) ||
                ctx.Client.CurrentApplication.Team != null &&
                ctx.Client.CurrentApplication.Team.Members.Any(x => x.User.Id == ctx.Member.Id));
    }

}