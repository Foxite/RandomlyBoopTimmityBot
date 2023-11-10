using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

string botToken = Environment.GetEnvironmentVariable("BOT_TOKEN")!;
ulong targetId = ulong.Parse(Environment.GetEnvironmentVariable("TARGET_ID")!);
string message = Environment.GetEnvironmentVariable("MESSAGE_CONTENTS")!;

var random = new Random();
var discord = new DiscordClient(new DiscordConfiguration() {
	Token = botToken
});

await discord.ConnectAsync();

while (true) {
	await Task.Delay(TimeSpan.FromHours(random.NextDouble() * 22 + 2));

	// Find a guild where the target is and we can DM them
	foreach (DiscordGuild guild in discord.Guilds.Values) {
		DiscordMember? targetMember;
		try {
			targetMember = await guild.GetMemberAsync(targetId);
		} catch (NotFoundException) {
			continue;
		}
		
		if (targetMember != null) {
			await targetMember.SendMessageAsync(message);
		}
	}
}
