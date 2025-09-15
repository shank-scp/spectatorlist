using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabApi.Events.Handlers;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;

namespace SpectatorList
{
    public class EventHandler
    {
        private const string CoroutineTag = "Spectator-List"; 

        private Config _config => SpectatorList.Instance.Config;

        public EventHandler()
        {
            ServerEvents.RoundStarted += OnRoundStarted;
        }

        ~EventHandler()
        {
            ServerEvents.RoundStarted -= OnRoundStarted;
            Timing.KillCoroutines(CoroutineTag);
        }

        private void OnRoundStarted() => Timing.RunCoroutine(DoList().CancelWith(Server.Host.GameObject), CoroutineTag);

        private IEnumerator<float> DoList()
        {
            while (true)
            {
                foreach (Player player in Player.List)
                {
                    if (player.IsAlive == false ||  _config.HiddenFor.Contains(player.Team)) continue;

                    int count = player.CurrentSpectators.Count(p => p.Role != RoleTypeId.Overwatch);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(count == 0
                        ? _config.NoSpectators
                        : _config.Spectators.Replace("%amount%", count.ToString()));

                    foreach (Player spectator in player.CurrentSpectators.Where(p => p.Role != RoleTypeId.Overwatch))
                        sb.AppendLine(_config.PlayerDisplay.Replace("%name%", spectator.DisplayName));

                    player.SendHint(_config.FullText.Replace("%display%", sb.ToString()), _config.RefreshRate + 0.15f);
                }

                yield return Timing.WaitForSeconds(_config.RefreshRate);
            }
        }
    }
}