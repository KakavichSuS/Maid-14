using Content.Shared._White.UserInterface;
using Robust.Shared.Configuration;

namespace Content.Shared._White;

[CVarDefs]
public sealed class WhiteCVars
{
    public static readonly CVarDef<float> GhostRespawnTime =
        CVarDef.Create("ghost.respawn_time", 15f, CVar.SERVERONLY);

    public static readonly CVarDef<int> GhostRespawnMaxPlayers =
        CVarDef.Create("ghost.respawn_max_players", 40, CVar.SERVERONLY);

    public static readonly CVarDef<EmotesMenuType> EmotesMenuStyle =
        CVarDef.Create("interface.emotes_menu_style", EmotesMenuType.Window, CVar.CLIENT | CVar.ARCHIVE);
}
