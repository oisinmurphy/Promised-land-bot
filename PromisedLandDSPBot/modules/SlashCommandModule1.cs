﻿using System.ComponentModel.DataAnnotations;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;


namespace PromisedLandDSPBot.Modules;
// please read https://github.com/DSharpPlus/DSharpPlus/tree/master/DSharpPlus.SlashCommands before attempting to edit this.

public class SlashCommandModule1 : ApplicationCommandModule
{

    // README - Put functions that are debug in nature here - only available to bot devs in Promised Land Server
    [SlashCommandGroup("debug", "Debug Functions - Special Access Only"),
     RequireRoles(RoleCheckMode.All,
         983462019250421800)] // This role is the "bot dev" permission role in Promised Land.
    public class DebugModule : ApplicationCommandModule
    {
        [SlashCommand("embedtest", "Made to test how embeds work in this framework."),
         RequirePermissions(Permissions.SendMessages)]
        //[RequireOwner] // this is owner of the bot, not owner of the server - so only the person who made the bot on the developer portal can access these commands.
        public async Task EmbedTestCommand(InteractionContext ctx,
            [Option("content", "This is the content of your embed post.")] string embedContent = "UNSET")
        {
            // CreateResponseAsync has a few overloads... one which helps here is shown below.
            var de = new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Blurple,
                    Author = new DiscordEmbedBuilder.EmbedAuthor()
                    {
                        IconUrl = ctx.Member.AvatarUrl,
                        Name = ctx.Member.DisplayName,
                        //rl = ctx.Member.Username
                    },
                    Title = "Embed Test",
                    Footer = new DiscordEmbedBuilder.EmbedFooter() { Text = ctx.Token },


                }
                .AddField("Content", $"{embedContent}")
                .AddField("Not In Line", "New Content Zone", true)
                .AddField("In Line", "This is in line", true)
                .Build();

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().AddEmbed(de));
        }

        [SlashCommand("modaltest", "Made to test how modals work in this framework.")]
        public async Task ModalTestCommand(InteractionContext ctx,
            [Option("title", "The title of the Modal")]
            string title = "Modal Title")
        {
            var response = new DiscordInteractionResponseBuilder();
            response
                .WithTitle("Super cool modal!")
                .WithCustomId("my-modal")
                .AddComponents(new TextInputComponent(label: "Favorite food", customId: "fav-food",
                    placeholder: "Pizza, Icecream, etc", max_length: 30))
                .AddComponents(new TextInputComponent("Why?", "why-fav", "Because it tastes good", required: false,
                    style: TextInputStyle.Paragraph));

            await ctx.CreateResponseAsync(InteractionResponseType.Modal, response);

        }

    }
}