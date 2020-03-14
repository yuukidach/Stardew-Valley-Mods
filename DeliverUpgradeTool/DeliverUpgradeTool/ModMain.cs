using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;

namespace DeliverUpgradeTool
{
    public class ModMain : Mod
    {
        private ToolHandler _toolHandler;
        private MailHandler _mailHandler;

        private string _mailContent = "Hello @, " +
                                      "^^Since your mailbox is too small, I've directly put your tool into your packet, please check it. " +
                                      "^^Have a nice day," +
                                      "^-Clint";

        private int _idx = 0;

        public override void Entry(IModHelper helper) {
            String TrashSpell = helper.Translation.Get("Trash");
            this.Monitor.Log($"{helper.Translation.Locale}", LogLevel.Debug);
            _toolHandler = new ToolHandler(TrashSpell);
            _mailHandler = new MailHandler();

            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;      
        }


        private void OnGameLaunched(object sender, GameLaunchedEventArgs e) {
            Helper.Content.AssetEditors.Add(_mailHandler);
        }


        private void OnDayStarted(object sender, DayStartedEventArgs e) {
            if (!Context.IsWorldReady || !_toolHandler.IsToolUpgradeFinished())
                return;

            _mailHandler.Add($"Upgraded Tool - {_idx++}", _mailContent);
            _mailHandler.Send();
            _toolHandler.GetUpgradeTool();
            _toolHandler.ClearToolInClint();
            Helper.Content.InvalidateCache("Data\\mail");
        }
    }
}