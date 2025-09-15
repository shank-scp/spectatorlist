using System.Collections.Generic;
using System.ComponentModel;
using PlayerRoles;

namespace SpectatorList
{
    public sealed class Config
    {
        [Description("A list of teams the hints should be hidden for")]
        public List<Team> HiddenFor { get; set; } = new List<Team>();

        [Description("How often in seconds to refresh the hud")]
        public float RefreshRate { get; set; } = 2;

        public string FullText { get; set; } = "<size=23><align=right>%display%</size><voffset=900> </voffset></align>";
        public string PlayerDisplay { get; set; } = "-> %name%";
        public string NoSpectators { get; set; } = "No one is currently spectating you.";
        public string Spectators { get; set; } = "👥 Spectators (%amount%)";
    }
}