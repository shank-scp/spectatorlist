using System;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace SpectatorList
{
    public class SpectatorList : Plugin<Config>
    {
        public static SpectatorList Instance { get; private set; }

        public override string Name => "Spectator List";
        public override string Author => "@misfiy";
        public override string Description => "A plugin that shows in screen the current players that are spectating you.";
        public override Version Version => new Version(1, 1, 1);
        public override Version RequiredApiVersion => new Version(LabApi.Features.LabApiProperties.CompiledVersion);

        private EventHandler _handler;

        public override void Enable()
        {
            Instance = this;
            _handler = new EventHandler();
            
            Logger.Info("Port by shank!");

        }

        public override void Disable()
        {
            _handler = null;
            Instance = null;

        }
    }
}